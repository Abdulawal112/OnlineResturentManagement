using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class CompanyProfileDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2,
        ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? VatRegNo { get; set; }
        public string? OwnerInfo { get; set; }
        public byte[]? Logo { get; set; }
        public string? LogoUrl { get; set; }
    }
}
