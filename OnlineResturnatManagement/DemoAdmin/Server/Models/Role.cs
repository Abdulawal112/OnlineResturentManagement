using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class Role:CreateUpdate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2,
       ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }

    }
}
