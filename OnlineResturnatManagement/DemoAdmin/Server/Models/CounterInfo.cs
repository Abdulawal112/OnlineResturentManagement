using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class CounterInfo
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2,
       ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string Name { get; set; }
        public string? MacAddress { get; set; }
        [DefaultValue(true)]
        public bool InActive { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
