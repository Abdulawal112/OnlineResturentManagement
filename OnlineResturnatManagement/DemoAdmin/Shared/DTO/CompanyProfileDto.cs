using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string? LogoUrl { get; set; }
      
        public FileData File { get; set; }
       
    }
    public class FileData
    {
        public byte[] Data { get; set; }
        public string? FileType { get; set; }
        public long Size { get; set; }
    }
}
