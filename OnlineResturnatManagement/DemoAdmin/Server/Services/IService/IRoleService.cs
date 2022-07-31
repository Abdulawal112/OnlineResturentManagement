using OnlineResturnatManagement.Server.Models;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role> GetRoleById(int id);
        public Task<Role> CreateRole(Role role);
        public Task<Role> UpdateRole(Role role);
        public Task<bool> IsExistRole(Role role);
    }
}
