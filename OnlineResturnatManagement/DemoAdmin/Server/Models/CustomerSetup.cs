using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
    public class CustomerSetup
    {
        public int Id { get; set; }
        public string? CardNo { get; set; }
        public string? Name { get; set; }
        public string PhoneNo { get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public Decimal? DiscountPercent { get; set; }
    }
}
