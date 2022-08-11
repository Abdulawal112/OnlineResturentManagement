using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class CreditCardDtos
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? BankName { get; set; }
        [Column("Type=decimal(18,4)")]
        public Decimal? BankCommission { get; set; }
    }
}
