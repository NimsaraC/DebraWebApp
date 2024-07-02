using System.ComponentModel.DataAnnotations;

namespace DebraWebApp.Models
{
    public class PartnerLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
