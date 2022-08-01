using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class NavigationMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Permitted { get; set; } = false;
        public bool Visible { get; set; } = false;
        public string RoleName { get; set; } = "";
        public int? ParentMenuId { get; set; }
        public string? ActionUrl { get; set; }

    }
}
