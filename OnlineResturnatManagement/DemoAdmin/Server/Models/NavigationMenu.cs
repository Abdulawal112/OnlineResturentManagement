using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
	[Table(name: "AspNetNavigationMenu")]
	public class NavigationMenu
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		[ForeignKey("ParentNavigationMenu")]
		public int? ParentMenuId { get; set; }

		public virtual NavigationMenu ParentNavigationMenu { get; set; }

		public string? ControllerName { get; set; }

		public string? ActionName { get; set; }

		public string Url { get; set; }

		public int DisplayOrder { get; set; }

		[NotMapped]
		public bool Permitted { get; set; }

		public bool Visible { get; set; }

		
	}
}