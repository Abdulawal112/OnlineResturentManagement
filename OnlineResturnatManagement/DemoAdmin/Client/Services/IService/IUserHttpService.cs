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
        public Task<ServiceResponse<RoleDto>> CreateRole(RoleDto role);
        public Task<ServiceResponse<RoleDto>> UpdateRole(RoleDto role);
    }
}
