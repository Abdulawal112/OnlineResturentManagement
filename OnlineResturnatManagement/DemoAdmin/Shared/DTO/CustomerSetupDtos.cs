using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class CustomerSetupDtos
    {
        public int Id { get; set; }
        public string? CardNo { get; set; }
        public string? Name { get; set; }
        public string PhoneNo { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public Decimal? DiscountPercent { get; set; }
    }
}
