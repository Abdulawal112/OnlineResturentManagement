using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = string.Empty;
        public int statusCode { get; set; }
    }
}
