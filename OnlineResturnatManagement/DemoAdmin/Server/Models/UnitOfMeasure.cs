using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class UnitOfMeasure : CreateUpdate
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2,
       ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string UOM { get; set; }
    }
}
