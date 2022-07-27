using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
	[Table(name: "AspNetRoleMenuPermission")]
	public class RoleMenuPermission
	{
		[Key]
		public int Id { get; set; }
		public int RoleId { get; set; }
		[ForeignKey("NavigationMenuId")]
		public int NavigationMenuId { get; set; }

		public NavigationMenu NavigationMenu { get; set; }
	}
}