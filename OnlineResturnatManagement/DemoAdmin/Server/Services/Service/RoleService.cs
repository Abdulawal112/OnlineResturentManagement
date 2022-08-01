using Microsoft.EntityFrameworkCore;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Shared.DTO;
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

        public async Task<IEnumerable<NavigationMenuDto>> GetNavigationManus(int roleId)
        {
            return await (from roles in _context.Roles
                                join rm in _context.RoleMenuPermission on roles.Id equals rm.RoleId
                                join menu in _context.NavigationMenu on rm.NavigationMenuId equals menu.Id
                                where roles.Id == roleId && menu.Url !=""
                                select new NavigationMenuDto                    
                                {
                                    Id = menu.Id,
                                    Name = menu.Name,
                                    Permitted = menu.Permitted,
                                    Visible = menu.Visible,
                                    RoleName = roles.Name,
                                })
                                .ToListAsync();
        }


        public async Task<IEnumerable<NavigationMenuDto>> UpdateNavigationMenu(List<NavigationMenuDto> menus, int roleId)
        {
            var GetMenusByRole = await _context.RoleMenuPermission.Where(role => role.RoleId == roleId).ToListAsync();

            _context.RoleMenuPermission.RemoveRange(GetMenusByRole);
            await _context.SaveChangesAsync();
            menus.ForEach(n => _context.RoleMenuPermission.Add(new RoleMenuPermission
            {
                RoleId = roleId,
                NavigationMenuId = n.Id,
            }));
            await _context.SaveChangesAsync();
            var response = await GetNavigationManus(roleId);
            return response.ToList();
        }

        public async Task<List<NavigationMenu>> GetMenus()
        {
            return await _context.NavigationMenu.Where(x=>x.Url !="").ToListAsync();
        }
    }
}
