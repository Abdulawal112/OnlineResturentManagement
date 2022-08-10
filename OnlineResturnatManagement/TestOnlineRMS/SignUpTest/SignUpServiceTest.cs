using Moq;
using OnlineResturnatManagement.Server.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineResturnatManagement.Server.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Win32;
using System.Collections;
using OnlineResturnatManagement.Client.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Xunit;
using OnlineResturnatManagement.Server.Controllers;
using AutoMapper;
using MOnlineResturnatManagement.Server.Services.RoleService;
using OnlineResturnatManagement.Server.Helper;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OnlineResturnatManagement.Shared.DTO;
using UserProfile = OnlineResturnatManagement.Server.Helper.UserProfile;
using Microsoft.Extensions.Configuration;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using User = OnlineResturnatManagement.Server.Models.User;
//using System.Web.Http.Results;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using StatusCodeResult = Microsoft.AspNetCore.Mvc.StatusCodeResult;
using Duende.IdentityServer.Services;
using ITokenService = OnlineResturnatManagement.Server.Helper.ITokenService;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TestOnlineRMS.SignUpTest
{
    public class SignUpServiceTest
    {
        //private IUserService _userService;
        //private ILoggerManager _logger;
        //private IDataAccessService _dataAccessService;
        //private IRoleService _roleService;
        //private IMapper _mapper;

        //private Mock<IConfiguration> _configuration;
        //private Mock<ITokenService> _tokenService;


        //private Mock<IUserService> _userservice;
        //private AccountsController controller;

        private IConfiguration _configuration;
        private ITokenService _tokenService;
        private IUserService _userservice;

        

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async void RegisterUser_USerNullOrModelStateInvalid_BadRequest()
        {
            var mockService = new Mock<IUserService>();

            //arrange
            var userForRegistrationDto = new UserForRegistrationDto
            {
                UserName = "",
                Password=""
            };

            //act
            var controller = new AccountsController(_configuration, _tokenService, mockService.Object);
            var result = await controller.RegisterUser(userForRegistrationDto);
            //assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async void RegisterUser_NameAlreadyExist_StatusCode400()
        {
            var mockService = new Mock<IUserService>();
            //arrange
            UserForRegistrationDto userForRegistrationDto = new UserForRegistrationDto()
            {
                UserName = "foysal",
                Password = "123456",
                ConfirmPassword = "123456"
            };
            User user = new User()
            {
                UserName = "foysal",
                PasswordHash = "123456",
            };
            //act
            var result1 = mockService.Setup(service => service.FindByNameAsync(userForRegistrationDto.UserName).Result).Returns(user);
            var controller = new AccountsController(_configuration, _tokenService, mockService.Object);

            var result = await controller.RegisterUser(userForRegistrationDto);
            //assert
             
            var badRequestResult= Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(badRequestResult.StatusCode == 400);
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async void RegisterUser_SaveUser_StatusCode200()
        {
            var mockService = new Mock<IUserService>();
            //arrange
            UserForRegistrationDto userForRegistrationDto = new UserForRegistrationDto()
            {
                UserName = "faruk",
                Password = "123456",
                ConfirmPassword = "123456"
            };
            User user = new User()
            {
                UserName = "foysal",
                PasswordHash = "123456",
            };
            //act
            var result1 = mockService.Setup(service => service.FindByNameAsync(user.UserName).Result).Returns(user);
            var result3 = mockService.Setup(service => service.CreateAsync(user,user.PasswordHash).Result).Returns(true);
            var controller = new AccountsController(_configuration, _tokenService, mockService.Object);

            var result = await controller.RegisterUser(userForRegistrationDto);
            //assert

            var okObjectResult = Assert.IsType<StatusCodeResult>(result);
            Assert.True(okObjectResult.StatusCode == 201);
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async void Login_UserNotFound_ReturnsOk200()
        {
            //arrange
           
            UserForAuthenticationDto userForRegistrationDto = new UserForAuthenticationDto()
            {
                UserName = "foysal",
                Password = "123456",
            };
            User user = new User()
            {
                UserName = "foysal",
                PasswordHash = "123456",
            };
            
            var userServiceMock = new Mock<IUserService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var result = userServiceMock.Setup(service => service.FindByNameAsync(user.UserName).Result).Returns(user);
            var result1 = userServiceMock.Setup(service => service.CheckPasswordAsync(user,user.PasswordHash)).Returns(true);

            var key = Encoding.UTF8.GetBytes("CodeMazeSecretKey");
            var secret = new SymmetricSecurityKey(key);
            var credentials= new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserName)
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: "CodeMazeAPI",
                audience: "https://localhost:5011",
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(4)),
                signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256));
            ////////////
            // Act
            var result2 = tokenServiceMock.Setup(service => service.GetSigningCredentials()).Returns(credentials);
            var result3 = tokenServiceMock.Setup(service => service.GetClaims(user).Result).Returns(claims);
            var result4 = tokenServiceMock.Setup(service => service.GenerateTokenOptions(credentials, claims)).Returns(tokenOptions);

            var controller = new AccountsController(_configuration, tokenServiceMock.Object, userServiceMock.Object);

            
            IActionResult actionResult = controller.Login(userForRegistrationDto).Result;
            //assert
           var okObjectResult= Assert.IsType<OkObjectResult>(actionResult);
            Assert.True(okObjectResult.StatusCode == 200);
        }
        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async void Login_UserNotFound_ReturnsUnauthorized401()
        {
            //arrange

            UserForAuthenticationDto userForRegistrationDto = new UserForAuthenticationDto()
            {
                UserName = "foysal",
                Password = "12",
            };
            User user = new User()
            {
                UserName = "foysal",
                PasswordHash = "123456",
            };

            var userServiceMock = new Mock<IUserService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var result = userServiceMock.Setup(service => service.FindByNameAsync(user.UserName).Result).Returns(user);
            var result1 = userServiceMock.Setup(service => service.CheckPasswordAsync(user, user.PasswordHash)).Returns(true);

            

            var controller = new AccountsController(_configuration, tokenServiceMock.Object, userServiceMock.Object);


            IActionResult actionResult = controller.Login(userForRegistrationDto).Result;
            //assert
            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(actionResult);
            Assert.True(unauthorizedObjectResult.StatusCode == 401);
        }

        //public class AutomapperSingleton
        //{
        //    private static IMapper _mapper;
        //    public static IMapper Mapper
        //    {
        //        get
        //        {
        //            if (_mapper == null)
        //            {
        //                // Auto Mapper Configurations
        //                var mappingConfig = new MapperConfiguration(mc =>
        //                {
        //                    mc.AddProfile(new UserProfile());
        //                });
        //                IMapper mapper = mappingConfig.CreateMapper();
        //                _mapper = mapper;
        //            }
        //            return _mapper;
        //        }
        //    }
        //}
        //[Fact]
        //public async Task Create_WhenSlugIsInUse_ReturnsBadRequest()
        //{
        //    // Arrange
        //    string slug = "Some Slug";
        //    var mockRepo = new Mock<IRoleService>();
        //    mockRepo.Setup(repo => repo.IsSlugAvailable(slug)).Returns(false);
        //    var controller = new BlogPostController(mockRepo.Object);
        //    var model = new InputModel { Slug = slug }

        //    // Act
        //    ActionResult<Post> result = controller.Create(model);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //}

    }
}
