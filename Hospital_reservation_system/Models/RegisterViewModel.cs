using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "TC is required.")]
        [MinLength(11, ErrorMessage = "TC must be 11 characters.")]
        [MaxLength(11, ErrorMessage = "TC can be 11 characters.")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3, ErrorMessage = "Password can be min 6 characters.")]
        [MaxLength(16, ErrorMessage = "Password can be max 16 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Password is required.")]
        [MinLength(3, ErrorMessage = "Password can be min 6 characters.")]
        [MaxLength(16, ErrorMessage = "Password can be max 16 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }


    }
}
