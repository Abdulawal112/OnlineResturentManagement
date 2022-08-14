using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class Kitchen:CreateUpdate
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2,
       ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string Name { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? Printer { get; set; }

    }
}
