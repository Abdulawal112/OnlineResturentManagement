using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? BankName { get; set; }
        [Column("Type=decimal(18,4)")]
        public Decimal? BankCommission { get; set; }
    }
}
