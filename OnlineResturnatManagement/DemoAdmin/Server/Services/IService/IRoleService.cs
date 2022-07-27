using OnlineResturnatManagement.Server.Models;

namespace OnlineResturnatManagement.Server.Services.IService
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role> GetRoleById(int id);
        public Task<bool> CreateRole(Role role);
        public Task<bool> UpdateRole(Role role);
        public Task<bool> IsExistRole(Role role);
    }
}
