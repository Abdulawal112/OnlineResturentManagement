using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class ActiveModuleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        [Range(0, 100000000, ErrorMessage = "Price must be greter than zero !")]
        public decimal Payment { get; set; }
        [Range(0.01, 100000000, ErrorMessage = "Price must be greter than zero !")]
        public decimal Price { get; set; }
    }
}
