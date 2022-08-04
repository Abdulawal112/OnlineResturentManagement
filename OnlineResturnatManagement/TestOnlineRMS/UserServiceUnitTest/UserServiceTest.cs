using Moq;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;

namespace TestOnlineRMS.UserServiceUnitTest
{
    public class UserServiceTest
    {
        [Fact]
        public async void GetUser_GetUSer_ReturnUserInfo()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetUser(1)).ReturnsAsync(new UserDto { UserName = "foysal" });
            var user = userService.Object;
            var result = await user.GetUser(1);
            Assert.NotNull(result);
            Assert.Equal("foysal", result.UserName);
        }

        [Fact]
        public async void GetUSerByName_GetUserInfo_ReturnUser()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetUserByName("foysal")).ReturnsAsync(new User { UserName = "foysal", Email = "foysal@gmail.com" });
            var user = userService.Object;
            var result = await user.GetUserByName("foysal");
            Assert.NotNull(result);
            Assert.Equal("foysal@gmail.com", result.Email);
            Assert.Equal("foysal", result.UserName);
        }

        //[Fact]
        //public async void GetRoleAsync_GetRolesByUSers_ReturnUSerRoles()
        //{
        //    var userService = new Mock<IUserService>();
        //    var response = userService.Setup(_ => _.GetRolesAsync(new User { Id = 1, UserName = "admin" })).ReturnsAsync(new List<Role>()
        //    {
        //    new Role() {Id =1 ,Name = "Employee", NormalizedName = "EMPLOYEE"},
        //    new Role() { Id = 2 , Name = "User", NormalizedName = "USER" }
        //    }) ;
        //    var user = userService.Object;
        //    List<Role>lists =await user.GetRolesAsync(new User()
        //    {
        //        Id = 1,
        //        UserName = "admin"
        //    });
        //    Assert.NotNull(lists);
        //    Assert.Equal(2, lists.Count());
        //}
    }
}
