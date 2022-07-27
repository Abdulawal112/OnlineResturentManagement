using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineResturnatManagement.Shared.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } =String.Empty;
        public string Email { get; set; } = String.Empty;   
    }
}
