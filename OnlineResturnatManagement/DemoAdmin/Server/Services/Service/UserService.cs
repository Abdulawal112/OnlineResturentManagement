using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class UserService : IUserService
    {
        public ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToRoleAsync(User user, string role)
        {
            var roleId = 0;
            var result = await _context.Roles.Where(x => x.Name == role).FirstOrDefaultAsync();
            if (result == null)
            {
                var roleData = new Role
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                var data = await _context.Roles.AddAsync(roleData);
                await _context.SaveChangesAsync();
                roleId = roleData.Id;
            }
            else
            {
                roleId = result.Id;
            }


            var userRole = new UserRole
            {
                RoleId = roleId,
                UserId = user.Id,
            };
            await _context.UserRoles.AddAsync(userRole);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _context.Users.Where(x => x.PasswordHash == password).AnyAsync();
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            user.PasswordHash = password;
            user.EmailConfirmed = false;
            user.RefreshTokenExpiryTime = new DateTime();
            var userData = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();
            if (userData == null)
            {
                await _context.Users.AddAsync(user);
                return await _context.SaveChangesAsync() > 0;
            }
            else
                return false;

        }

        public async Task<User> FindByNameAsync(string userName)
        {
           var data= await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            return data;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            return await (from m in _context.Users
                          select new UserDto()
                          {
                              Id = m.Id,
                              UserName = m.UserName,
                              Email = m.Email
                          })
                               .ToListAsync();
        }

        public async Task<List<Role>> GetRolesAsync(User user)
        {
            var data = await (from roles in _context.Roles
                              join rp in _context.UserRoles on roles.Id equals rp.RoleId
                              join u in _context.Users on rp.UserId equals u.Id
                              where u.UserName == user.UserName && u.Id == user.Id
                              select roles)
                               .ToListAsync();
            return data;
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var result = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return new UserDto
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email
            };

        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        //Task<IEnumerable<UserDto>> IUserService.GetAllUserAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
