using Microsoft.AspNetCore.Mvc;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync(string search);
        Task<bool> CreateAsync(User user,string password);
        Task<bool> AddToRoleAsync(User user,string role);
        Task<bool> UpdateAsync(User user);
        Task<User> FindByNameAsync(string userName);
        bool CheckPasswordAsync(User user, string password);
        Task<List<Role>> GetRolesAsync(User user);

        Task<UserDto> GetUser(int userId);
        Task<User> GetUserByName(string name);
        Task<UserDto> UpdateUserWithRole(UserDto user);
        Task<IEnumerable<NavigationMenu>> GetUsersNavMenus(string userName);
    }
}
