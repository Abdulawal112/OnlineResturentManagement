using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using OnlineResturnatManagement.Server.Data;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO.RoleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OnlineResturnatManagement.Server.Helper.Permissions;
using User = OnlineResturnatManagement.Server.Models.User;

namespace MOnlineResturnatManagement.Server.Services.RoleService
{
	public class DataAccessService : IDataAccessService
	{
		private readonly IMemoryCache _cache;
		private readonly ApplicationDbContext _context;

		public DataAccessService(ApplicationDbContext context, IMemoryCache cache)
		{
			_cache = cache;
			_context = context;
		}

		public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
		{
			var isAuthenticated = principal.Identity.IsAuthenticated;
			if (!isAuthenticated)
			{
				return new List<NavigationMenuViewModel>();
			}

			var roleIds = await GetUserRoleIds(principal);

			var permissions = await _cache.GetOrCreateAsync("Permissions",
				async x => await (from menu in _context.NavigationMenu select menu).ToListAsync());

			var rolePermissions = await _cache.GetOrCreateAsync("RolePermissions",
				async x => await (from menu in _context.RoleMenuPermission select menu).Include(x => x.NavigationMenuId).ToListAsync());

			//var data = (from menu in rolePermissions
			//			join p in permissions on menu.NavigationMenuId equals p.Id
			//			where roleIds.Contains(menu.RoleId)
			//			select p)
			//				  .Select(m => new NavigationMenuViewModel()
			//				  {
			//					  Id = m.Id,
			//					  Name = m.Name,
			//					  Area = m.Area,
			//					  Visible = m.Visible,
			//					  IsExternal = m.IsExternal,
			//					  ActionName = m.ActionName,
			//					  ExternalUrl = m.ExternalUrl,
			//					  DisplayOrder = m.DisplayOrder,
			//					  ParentMenuId = m.ParentMenuId,
			//					  ControllerName = m.ControllerName,
			//				  }).Distinct().ToList();

			return new List<NavigationMenuViewModel>();
		}

		public async Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string userName,string Roles,string path)
		{
			var result = false;
			//var roleIds = await GetUserRoleIds(ctx);
			//var data = await (from menu in _context.RoleMenuPermission
			//				  where roleIds.Contains(menu.RoleId)
			//				  select menu)
			//				  .Select(m => m.NavigationMenu)
			//				  .Distinct()
			//				  .ToListAsync();

			//foreach (var item in data)
			//{
			//	result = (item.ControllerName == ctrl && item.ActionName == act);
			//	if (result)
			//	{
			//		break;
			//	}
			//}
			var RolesData = Roles;

            //var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
			string uName = userName == null ? "" : userName;
			//var userRoles = await _context.Roles.ToListAsync();
			var data = await (from roles in _context.Roles
							  join rp in _context.UserRoles on roles.Id equals rp.RoleId
							  join u in _context.Users on rp.UserId equals u.Id
                              join rmp in _context.RoleMenuPermission on roles.Id equals rmp.RoleId
                              join um in _context.NavigationMenu on rmp.NavigationMenuId equals um.Id
                              where u.UserName == uName && roles.Name == RolesData && roles.Name == RolesData
							select rp)
							.FirstOrDefaultAsync();

            
            //                 join um in _context.NavigationMenu on rmp.NavigationMenuId equals um.Id

            if (data != null)
			{
				result = true;
			}			 
            //foreach (var item in userRoles)
            //{
            //	bool v = (item.Name == Roles[i]);
            //	result = v;
            //	if (result)
            //	{
            //		break;
            //	}
            //}
            //_context.Users.Where(r => userRoleIds.Contains(Roles) && r.UserName == userName).Any();

            return result;
		}

		public async Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id)
		{
			//var items = await (from m in _context.NavigationMenu
			//				   join rm in _context.RoleMenuPermission
			//					on new { X1 = m.Id, X2 = id } equals new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
			//					into rmp
			//				   from rm in rmp.DefaultIfEmpty()
			//				   select new NavigationMenuViewModel()
			//				   {
			//					   Id = m.Id,
			//					   Name = m.Name,
			//					   Area = m.Area,
			//					   ActionName = m.ActionName,
			//					   ControllerName = m.ControllerName,
			//					   IsExternal = m.IsExternal,
			//					   ExternalUrl = m.ExternalUrl,
			//					   DisplayOrder = m.DisplayOrder,
			//					   ParentMenuId = m.ParentMenuId,
			//					   Visible = m.Visible,
			//					   Permitted = rm.RoleId == id
			//				   })
			//				   .AsNoTracking()
			//				   .ToListAsync();

			return new List<NavigationMenuViewModel>();
		}

		public async Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds)
		{
			//var existing = await _context.RoleMenuPermission.Where(x => x.RoleId == id).ToListAsync();
			//_context.RemoveRange(existing);

			//foreach (var item in permissionIds)
			//{
			//	await _context.RoleMenuPermission.AddAsync(new RoleMenuPermission()
			//	{
			//		RoleId = id,
			//		NavigationMenuId = item,
			//	});
			//}

			var result = await _context.SaveChangesAsync();

			// Remove existing permissions to roles so it can re evaluate and take effect
			_cache.Remove("RolePermissions");

			return result > 0;
		}

		private async Task<List<int>> GetUserRoleIds(ClaimsPrincipal ctx)
		{
			//////////////
			var userId = Convert.ToInt32(GetUserId(ctx));
			var data = await (from role in _context.UserRoles
							  where role.UserId == userId
							  select role.RoleId).ToListAsync();

			return data;
		}

		private string GetUserId(ClaimsPrincipal user)
		{
			return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}