using System.ComponentModel.DataAnnotations;

namespace Getri_TokenAuthentication.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be atleast 3 characters")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be atleast 5 characters")]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "You must provide password between 8 and 20 charachers")]
        public string Password { get; set; }

    }
}
