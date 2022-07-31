using OnlineResturnatManagement.Shared.DTO;
using OnlineResturnatManagement.Shared;

namespace OnlineResturnatManagement.Client.Services.IService
{
    public interface IUserHttpService
    {
        public Task<ServiceResponse<List<UserDto>>> GetAllUser();
        public Task<ServiceResponse<List<RoleDto>>> GetRoles();
        public Task<ServiceResponse<UserDto>> UpdateUserWithRole(UserDto userDto);

        public Task<ServiceResponse<UserDto>> GetUserById(int id);

        public Task<ServiceResponse<List<NavigationMenuDto>>> GetAllMenu();
        public Task<ServiceResponse<List<NavigationMenuDto>>> GetMenuByRoleId(int id);
        public Task<ServiceResponse<List<NavigationMenuDto>>> UpdateRoleMenus(int id,List<NavigationMenuDto> navigationMenus);
        public Task<ServiceResponse<RoleDto>> CreateRole(RoleDto role);
        public Task<ServiceResponse<RoleDto>> UpdateRole(RoleDto role);
        // For Sidebar Menu
        public Task<ServiceResponse<List<NavigationMenuDto>>> GetUserMenu(string name);
    }
}
