using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class CompanyProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500,MinimumLength = 2,
        ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? VatRegNo { get; set; }
        public string? OwnerInfo { get; set; }
        public string? LogoUrl { get; set; }

    }
}
