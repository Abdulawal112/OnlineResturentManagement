using System.ComponentModel;

namespace OnlineResturnatManagement.Server.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        public bool Default { get; set; } 
    }
}
