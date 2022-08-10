using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
using static OnlineResturnatManagement.Server.Helper.Permissions;

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
            var result = new Role();
            result = await _context.Roles.Where(x => x.Name == role).FirstOrDefaultAsync();
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

        public bool CheckPasswordAsync(User user, string password)
        {

            var encryptPassword = EncryptPassword.EncryptStringToBytes(password, user.HashKey);
            return user.PasswordHash == encryptPassword; /*_context.Users.Where(x => x.PasswordHash == encryptPassword).AnyAsync()*/;
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            string encoded = Convert.ToBase64String(bytes);

            user.HashKey = encoded;


            //user.HashPassword = AESEncrytDecry.EncryptStringToBytes(user.Password, user.HashKey);
            user.PasswordHash = EncryptPassword.EncryptStringToBytes(password, user.HashKey);
            user.EmailConfirmed = false;
            user.RefreshTokenExpiryTime = new DateTime();
            user.CreateBy = "";
            user.CreateDate = DateTime.Now;
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
            //var userData = await _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefaultAsync();
            //if (userData != null)
            //{
            //    return false;
            //}
            //else
            //{

            //}

        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
             
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync(string searchString)
        {
            var users= (from u in _context.Users
                          join rp in _context.UserRoles on u.Id equals rp.UserId
                          join r in _context.Roles on rp.RoleId equals r.Id
                          select new UserDto()
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              Email = u.Email,
                              PhoneNumber = u.PhoneNumber,
                              RoleName = r.Name
                          });
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName!.Contains(searchString) || s.PhoneNumber!.Contains(searchString) || s.RoleName!.Contains(searchString));
            }
            return await users.ToListAsync();
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
            var data = await (from u in _context.Users
                              join rp in _context.UserRoles on u.Id equals rp.UserId into gj
                              from x in gj.DefaultIfEmpty()
                                  //join r in _context.Roles on rp.RoleId equals r.Id
                              where u.Id == userId
                              select new UserDto
                              {
                                  Id = u.Id,
                                  UserName=u.UserName,
                                  Email=u.Email,
                                  PhoneNumber =u.PhoneNumber,
                                  RoleId = (x == null ? 0 : x.RoleId)
                              })
                             .FirstOrDefaultAsync();
            return data;
        }
        public async Task<User> GetUserByName(string name)
        {

            return await _context.Users.Where(x => x.UserName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NavigationMenu>> GetUsersNavMenus(string userName)
        {
            
            var result = await _context.NavigationMenu.FromSqlRaw(@"SELECT distinct nm.Name,nm.Id,nm.ControllerName,DisplayOrder,Url,Visible,ParentMenuId,ActionUrl,NavIcon,nm.ModuleId
                                                              FROM [DemoAdminDB].[dbo].[UserRoles] ur
                                                              inner join RoleMenuPermission rm on rm.RoleId = ur.RoleId
                                                              inner join NavigationMenu nm on nm.Id = rm.NavigationMenuId or (nm.ActionUrl is null)
                                                              inner join Users u on u.Id = ur.UserId
                                                              where LOWER(u.UserName) = '" + userName.ToLower()+"'").ToListAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDto> UpdateUserWithRole(UserDto user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var FindUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (FindUser == null)
                {
                    transaction.Rollback();
                    return null;
                }

                FindUser.UserName = user.UserName;
                FindUser.Email = user.Email;
                FindUser.PhoneNumber = user.PhoneNumber == "" ? FindUser.PhoneNumber : user.PhoneNumber;
                FindUser.UpdateBy = user.UpdateBy;
                FindUser.UpdateDate = user.UpdateDate;
                if(user.Password != "")
                {
                    Guid guid = Guid.NewGuid();
                    byte[] bytes = guid.ToByteArray();
                    string encoded = Convert.ToBase64String(bytes);

                    FindUser.HashKey = encoded;
                    FindUser.PasswordHash = EncryptPassword.EncryptStringToBytes(user.Password, FindUser.HashKey);
                }
                _context.Users.Update(FindUser);
                var result = await _context.SaveChangesAsync() > 0;

                if(result && (user.RoleId !=null || user.RoleId > 0))
                {
                    var commandText = "delete from UserRoles where UserId =" + user.Id + "";
                    //var name = new SqlParameter("@CategoryName", "Test");
                    _context.Database.ExecuteSqlRaw(commandText);
                    var result2 = await _context.SaveChangesAsync() > 0;
                    var userRole = new UserRole
                    {
                        RoleId = (int)user.RoleId,
                        UserId = user.Id,
                    };
                    await _context.UserRoles.AddAsync(userRole);
                    await _context.SaveChangesAsync();
                }

                transaction.Commit();
                return user;
            }
            catch
            {
                return null;
                transaction.Rollback();
               
            }

        }

        //Task<IEnumerable<UserDto>> IUserService.GetAllUserAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
