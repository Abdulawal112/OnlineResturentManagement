using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class ActiveModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public string Payment { get; set; }
        [Range(0.01, 100000000, ErrorMessage = "Price must be greter than zero !")]
        public decimal Price { get; set; }
    }
}
