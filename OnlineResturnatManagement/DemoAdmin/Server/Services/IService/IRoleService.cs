using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role> GetRoleById(int id);
        public Task<Role> CreateRole(Role role);
        public Task<Role> UpdateRole(Role role);
        public Task<bool> IsExistRole(Role role);
        public Task<List<NavigationMenu>> GetMenus();
        public Task<IEnumerable<NavigationMenuDto>> GetNavigationManus(int roleId);
        public Task<IEnumerable<NavigationMenuDto>> UpdateNavigationMenu(List<NavigationMenuDto>menus ,int roleId);
    }
}
