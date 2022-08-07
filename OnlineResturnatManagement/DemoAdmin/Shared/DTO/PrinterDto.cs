using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class PrinterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; } = false;
    }
}
