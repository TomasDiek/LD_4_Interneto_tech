using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LD_4_Interneto_tech.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }
        public string? ResetToken { get; internal set; }
        [Required]
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}