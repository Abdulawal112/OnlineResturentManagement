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
            //var ListOfNavMenu = await _context.NavigationMenu.Where(x => x.RoleId == roleId).ToListAsync();
            //var result = from menus in ListOfNavMenu
            //              select new NavigationMenuDto()
            //             {
            //                 Id = menus.Id,
            //                 Name = menus.Name,
            //                 Permitted = menus.Permitted,
            //                 Visited = menus.Visible,

            //             };
            return null;
        }

        public async Task<NavigationMenuDto> UpdateNavigationMenu(int menuId , int roleId)
        {
            //var GetMenu = await _context.NavigationMenu.Where(x => x.Id == menuId).FirstOrDefaultAsync();
            //GetMenu.RoleId = roleId;
            //_context.NavigationMenu.Update(GetMenu);
            //await _context.SaveChangesAsync();
            //return new NavigationMenuDto
            //{
            //    Id = GetMenu.Id,
            //    Name = GetMenu.Name,
            //    DisplayOrder = GetMenu.DisplayOrder,
            //    Permitted = GetMenu.Permitted,
            //    Visited = GetMenu.Visible
            //};
            return null;
        }
    }
}
