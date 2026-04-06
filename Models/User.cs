using System.ComponentModel.DataAnnotations;

namespace QL_CUA_HANG_BAN_XE_DAP.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}