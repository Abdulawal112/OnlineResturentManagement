

using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineResturnatManagement.Server.Models
{
    public class User :CreateUpdate
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2,
       ErrorMessage = "*Name must be MinimumLength 2 or More.")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }=false;
        public string? HashKey { get; set; }
        public string PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
