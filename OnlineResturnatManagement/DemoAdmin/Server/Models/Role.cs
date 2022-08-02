using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class Role:CreateUpdate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

    }
}
