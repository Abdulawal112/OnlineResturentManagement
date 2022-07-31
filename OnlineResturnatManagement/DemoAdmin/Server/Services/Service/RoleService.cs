using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using System.Xml.Linq;

namespace OnlineResturnatManagement.Server.Services.Service
{
    public class RoleService : IRoleService
    {
        public ApplicationDbContext _context;
        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role role)
        {
           role.NormalizedName=role.Name.ToUpper();
            await _context.Roles.AddAsync(role);
            var result =await _context.SaveChangesAsync()>0;
            if(result)
                return role;
            else
                return new Role();
            
            
        }

        public async Task<bool> IsExistRole(Role role)
        {
            bool isExist = false;
            if (role.Id == 0)
            {
                isExist = await _context.Roles.FirstOrDefaultAsync(x => x.Name == role.Name) != null ? true : false;
            }
            else
            {
                isExist = await _context.Roles.FirstOrDefaultAsync(x => x.Name == role.Name && x.Id != role.Id) != null ? true : false;
            }
            return isExist;

        }

        public async Task<Role> GetRoleById(int id)
        {
           return await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> UpdateRole(Role role)
        {
            role.NormalizedName = role.Name.ToUpper();
            _context.Roles.Update(role);
           var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return role;
            else
                return new Role();

        }

        
    }
}
