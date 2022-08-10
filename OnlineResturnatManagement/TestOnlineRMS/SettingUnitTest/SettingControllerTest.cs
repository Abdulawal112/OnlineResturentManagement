using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using MOnlineResturnatManagement.Server.Services.RoleService;
using Moq;
using OnlineResturnatManagement.Server.Controllers;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineRMS.UserServiceUnitTest;

namespace TestOnlineRMS.SettingUnitTest
{
    public class SettingControllerTest
    {
        private ISettingSrevice _settingSrevice;
        private IMapper _mapper;
        private readonly ICashHelper _cacheService;
        private IWebHostEnvironment _webHostEnvironment;
        [Fact]
        public async void GetAllActiveModule_ListOfActiveModule_ReturnOk200()
        {
            var mockService = new Mock<ISettingSrevice>();
            mockService.Setup(_ => _.GetActiveModules()).ReturnsAsync(new List<ActiveModule> { new ActiveModule { Id = 1, Name = "Accounts management", Status=true,Price=0,Payment=0 }, new ActiveModule { Id = 2, Name = "Accounts management2", Status = true, Price = 0, Payment = 0 } });
            var controller = new SettingsController(mockService.Object, _mapper, _webHostEnvironment,_cacheService);
            var result = await controller.GetAllActiveModule();
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            Assert.True(okObjectResult.StatusCode == 200);
        }
        [Fact]
        public async void SaveCompanyProfile_CompanyInfo_BadRequest()
        {
            var mockService = new Mock<ISettingSrevice>();
            var mockService1 = new Mock<ICashHelper>();
            var company = new CompanyProfile
            {
                Id = 0,
                Name = "MediaSoft",
                VatRegNo="324234",
                Address="Dhaka",
                LogoUrl = "",
                OwnerInfo="",
            };
            var companyDto = new CompanyProfileDto
            {
                Id = 0,
                Name = "MediaSoft",
                VatRegNo = "324234",
                Address = "Dhaka",
                LogoUrl = "",
                OwnerInfo = "",
            };
            mockService.Setup(_ => _.SaveCompanyProfile(company)).ReturnsAsync(company);
            var controller = new SettingsController(mockService.Object, AutomapperSingletonNew.Mapper, _webHostEnvironment, mockService1.Object);
            var result = await controller.SaveCompanyProfile(companyDto);
            var objectResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.True(objectResult.StatusCode == 400);
        }
        [Fact]
        public async void SaveCompanyProfile_GetCompanyPorfile_ReturnCompanyProfile()
        {
            var company = new CompanyProfile
            {
                Id = 0,
                Name = "MediaSoft",
                VatRegNo = "324234",
                Address = "Dhaka",
                LogoUrl = "",
                OwnerInfo = "",
            };
            var settingService = new Mock<ISettingSrevice>();
            settingService.Setup(_ => _.SaveCompanyProfile(company)).ReturnsAsync(company);
            var user = settingService.Object;
            var result = await user.SaveCompanyProfile(company);
            Assert.NotNull(result);
            Assert.Equal("MediaSoft", result.Name);
            Assert.Equal("Dhaka", result.Address);
        }
        //[Fact]
        //public async void GetCompanyProfile_CompanyProfile_ReturnOk200()
        //{
        //    var mockService = new Mock<ISettingSrevice>();
        //    var mockService2 = new Mock<IWebHostEnvironment>();
        //    var mockService3 = new Mock<ICashHelper>();



        //    mockService.Setup(_ => _.GetCompanyProfile()).ReturnsAsync( new CompanyProfile { Id = 1, Name = "MediaSoft", Address = "Dhaka", LogoUrl = "", OwnerInfo = "",VatRegNo="353533534" });
        //    var controller = new SettingsController(mockService.Object, AutomapperSingletonNew.Mapper, mockService2.Object, mockService3.Object);
        //    var result = await controller.GetCompanyProfile();
        //    var okObjectResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.NotNull(result);
        //    Assert.True(okObjectResult.StatusCode == 200);
        //}

    }
}
