using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineResturnatManagement.Server.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        //[NotMapped]
        //public List<User> Users { get; set; }
        //[NotMapped]
        //public List<Role> Roles { get; set; }
    }
}
