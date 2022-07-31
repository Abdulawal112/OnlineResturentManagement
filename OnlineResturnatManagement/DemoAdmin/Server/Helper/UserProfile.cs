using AutoMapper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Helper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
