using System.ComponentModel.DataAnnotations;

namespace DebraWebApp.Models
{
    public class AdminLogin
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
