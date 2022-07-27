using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class UserMenuDto
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Url { get; set; }
        public int? ParentID { get; set; }
        public int? Serial { get; set; }
    }
}
