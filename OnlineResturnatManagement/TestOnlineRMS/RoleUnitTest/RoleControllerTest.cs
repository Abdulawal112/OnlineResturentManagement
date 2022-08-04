using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOnlineResturnatManagement.Server.Services.RoleService;
using Moq;
using OnlineResturnatManagement.Server.Controllers;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit.Sdk;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestOnlineRMS.UserServiceUnitTest
{
    public class AutomapperSingleton
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new UserProfile());
                    });
                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }
                return _mapper;
            }
        }
        public class RoleControllerTest
        {
            private ILoggerManager _logger;
            private IDataAccessService _dataAccessService;
            private IRoleService _roleService;
            private IMapper _mapper;
            private readonly ICashHelper _cacheService;
            private IUserService _userService;

            /*        public UserControllerTest()
                    {
                        var mockService = new Mock<IUserService>();
                        var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
                    }*/


            [Fact]
            public async void GetRoles_ListOfRoles_ReturnOk200()
            {
                var mockService = new Mock<IRoleService>();
                var mockService2 = new Mock<ICashHelper>();
                mockService.Setup(_ => _.GetRoles()).ReturnsAsync(new List<Role> { new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "Viewer" } });
                var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, _mapper, mockService2.Object);
                var result = await controller.GetAllRoles();
                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(result);
                Assert.True(okObjectResult.StatusCode == 200);
            }
            //[Fact]
            //public async void GetRoles_ListOfRoles_ReturnNull()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    var mockService2 = new Mock<ICashHelper>();
            //    mockService.Setup(_ => _.GetRoles()).ReturnsAsync(null);
            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, _mapper, mockService2.Object);
            //    var result = await controller.GetAllRoles();
            //    var okObjectResult = Assert.IsType<NoContentResult>(result);
            //    Assert.True(okObjectResult.StatusCode == 204);
            //}


            //Add by role Method started here
            [Fact]
            public async void AddRole_SendValidInfo_Return201()
            {
                var mockService = new Mock<IRoleService>();
                var mockService2 = new Mock<ICashHelper>();
                var role = new Role
                {
                    Id = 0,
                    Name = "Viewer",
                };
                var roleDto = new RoleDto
                {
                    Id = 0,
                    Name = "Viewer",
                   
                };
                var createRole = new Role
                {
                    Id = 1,
                    Name = "Viewer",
                    
                };
                mockService.Setup(_ => _.CreateRole(role).Result).Returns(role);

                var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, AutomapperSingleton.Mapper, mockService2.Object);
                var result = await controller.AddRole(roleDto);

                var okObjectResult = Assert.IsType<CreatedResult>(result);
                Assert.True(okObjectResult.StatusCode == 201);
            }
            //Add by role Method started here
            [Fact]
            public async void EditRole_SendValidInfo_Return201()
            {
                var mockService = new Mock<IRoleService>();
                var mockService2 = new Mock<ICashHelper>();
                var role = new Role { Id = 1, Name = "Viewer" };
                mockService.Setup(_ => _.UpdateRole(role)).ReturnsAsync(new Role { Id = 1, Name = "Viewer" });

                var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, AutomapperSingleton.Mapper, mockService2.Object);
                var result = await controller.EditRole(new RoleDto { Id = 1, Name = "Viewer" });

                var okObjectResult = Assert.IsType<OkObjectResult>(result);
                Assert.True(okObjectResult.StatusCode == 200);
            }
            //[Fact]
            //public async void IsExistRole_SendExistInfo_Return409()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    var mockService2 = new Mock<ICashHelper>();
            //    //mockService.Setup(_ => _.CreateRole(new Role { Id = 1, Name = "Viewer" })).ReturnsAsync(new Role { Id = 1, Name = "Viewer" });
            //    mockService.Setup(_ => _.IsExistRole(new Role { Id = 1, Name = "Viewer",CreateBy="",CreateDate=new DateTime(),UpdateBy="",UpdateDate=new DateTime(),NormalizedName=null })).ReturnsAsync(true);

            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, AutomapperSingleton.Mapper, mockService2.Object);
            //    var result = await controller.AddRole(new RoleDto { Id = 2, Name = "Viewer", CreateBy = "", CreateDate = new DateTime(), UpdateBy = "", UpdateDate = new DateTime() });

            //    var okObjectResult = Assert.IsType<ConflictResult>(result);
            //    Assert.True(okObjectResult.StatusCode == 409);
            //}




            ////Add by role Method started here
            //[Fact]
            //public async void UpdateRole_SendValidInfo_Return200()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    mockService.Setup(_ => _.UpdateRole(new Role { Id = 2, Name = "Viewer" }));

            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, AutomapperSingleton.Mapper, _cacheService);
            //    var result = await controller.EditRole(new RoleDto { Id = 2, Name = "Viewer" });

            //    var okObjectResult = Assert.IsType<OkObjectResult>(result);
            //    Assert.NotNull(okObjectResult);
            //    Assert.True(okObjectResult.StatusCode == 200);
            //}
            ///*USerController GetUser() Methods Tests are here*/
            //[Fact]
            //public async void GetUser_UserInformation_Return200()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUser(1)).ReturnsAsync(new UserDto { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetUser(1);
            //    var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            //    Assert.True(objectResult.StatusCode == 200);
            //}
            //[Fact]
            //public async void GetUser_UserInformation_Return400()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUser(1)).ReturnsAsync(new UserDto { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetUser(5);
            //    var objectResult = Assert.IsType<StatusCodeResult>(result.Result);
            //    Assert.True(objectResult.StatusCode == 400);
            //}

            //[Fact]
            //public async void GetUSer_UserInformation_ReturnBadRequest()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUser(1)).ReturnsAsync(new UserDto { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetUser(0);
            //    var objectResult = Assert.IsType<BadRequestResult>(result.Result);
            //    Assert.True(objectResult.StatusCode == 400);
            //}

            ///*end */

            ///*  GetUserByName for profile information */

            //[Fact]
            //public async void GetUserByName_UserInfo_Return200()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUserByName("tutul")).ReturnsAsync(new User { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, AutomapperSingleton.Mapper, _cacheService);
            //    var result = await controller.GetUserByName("tutul");
            //    var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            //    Assert.True(okObjectResult.StatusCode == 200);
            //}

            //[Fact]
            //public async void GetUserByName_EmptyNameSend_ReturnBadRequest()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUserByName("tutul")).ReturnsAsync(new User { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetUserByName("");
            //    var okObjectResult = Assert.IsType<BadRequestResult>(result.Result);
            //    Assert.True(okObjectResult.StatusCode == 400);
            //}

            //[Fact]
            //public async void GetUserByName_DontFindInDatabase_Return400()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUserByName("tutul")).ReturnsAsync(new User { Id = 1, UserName = "tutul" });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetUserByName("mehedi");
            //    var okObjectResult = Assert.IsType<StatusCodeResult>(result.Result);
            //    Assert.True(okObjectResult.StatusCode == 400);
            //}

            ////end

            ///*GetMenusByUser method started here*/
            //[Fact]
            //public async void GetMenusByUser_SendingEmptyName_ReturnBadRequest()
            //{
            //    var mockService = new Mock<IUserService>();
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetMenusByUser("");
            //    var okObjectResult = Assert.IsType<BadRequestResult>(result);
            //    Assert.True(okObjectResult.StatusCode == 400);
            //}

            //[Fact]
            //public async void GetMenusByUser_SendValidName_ReturnListOfNavMenus()
            //{
            //    var mockService = new Mock<IUserService>();
            //    mockService.Setup(_ => _.GetUsersNavMenus("tutul")).ReturnsAsync(new List<NavigationMenu> { new NavigationMenu { Id = 1, Name = "Employee" }, new NavigationMenu { Id = 2, Name = "Users" } });
            //    var controller = new UsersController(mockService.Object, _logger, _dataAccessService, _roleService, AutomapperSingleton.Mapper, _cacheService);
            //    var result = await controller.GetMenusByUser("tutul");
            //    var okObjectResult = Assert.IsType<OkObjectResult>(result);
            //    Assert.NotNull(result);
            //    Assert.True(okObjectResult.StatusCode == 200);
            //}

            ////end

            ////UpdateRoleMenu by user Method started here
            //[Fact]
            //public async void UpdateRoleMenu_SendValidInfo_Return200()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    mockService.Setup(_ => _.UpdateNavigationMenu
            //    (new List<NavigationMenuDto>
            //    { new NavigationMenuDto { Id = 1, Name = "Employee" },
            //    new NavigationMenuDto { Id = 2, Name = "Users" } }, 1)).
            //    ReturnsAsync(new List<NavigationMenuDto> {
            //    new NavigationMenuDto { Id = 1, Name = "Employee" },
            //    new NavigationMenuDto { Id = 2, Name = "Users" } });

            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, _mapper, _cacheService);
            //    var result = await controller.UpdateRoleMenu(1, new List<NavigationMenuDto>
            //    { new NavigationMenuDto { Id = 1, Name = "Employee" },
            //    new NavigationMenuDto { Id = 2, Name = "Users" } });

            //    var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            //    Assert.NotNull(okObjectResult);
            //    Assert.True(okObjectResult.StatusCode == 200);
            //}

            //[Fact]
            //public async void UpdateRoleMenu_EmptyRoleIdOrMenus_ReturnBadRequest()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    mockService.Setup(_ => _.UpdateNavigationMenu(new List<NavigationMenuDto>(), 1)).Verifiable();
            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, _mapper, _cacheService);
            //    var result = await controller.UpdateRoleMenu(1, new List<NavigationMenuDto>());
            //    /*mockService.Verify(m => m.UpdateNavigationMenu(It.IsAny<List<NavigationMenuDto>>(), It.IsAny<int>()),Times.Once);*/
            //    var okObjectResult = Assert.IsType<BadRequestResult>(result.Result);
            //    Assert.True(okObjectResult.StatusCode == 400);
            //}

            ////end

            ///*GetAllMenu unittest*/
            //[Fact]
            //public async void GetAllMenu_EmptyMenus_Return400()
            //{
            //    var mockService = new Mock<IRoleService>();
            //    var controller = new UsersController(_userService, _logger, _dataAccessService, mockService.Object, _mapper, _cacheService);
            //    var result = await controller.GetAllMenu();
            //    var okResult = Assert.IsType<StatusCodeResult>(result);
            //    Assert.True(okResult.StatusCode == 400);
            //}
            ////end

            ////GetMenus by RoleID
            //[Fact]
            //public async void GetNavigationMenus_GivenRoleIdGetMenus_Return200()
            //{
            //    var controller = new UsersController(_userService, _logger, _dataAccessService, _roleService, _mapper, _cacheService);
            //    var result = await controller.GetNavigationMenus(1);
            //    var okResult = Assert.IsType<StatusCodeResult>(result);
            //    Assert.True(okResult.StatusCode == 200);

            //}


        }
    }
}
