using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

    }
}
