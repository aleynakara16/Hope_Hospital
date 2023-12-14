using Hospital_reservation_system.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Models
{
    public class DoctorViewModel
    {
        [Required(ErrorMessage = "TC is required.")]
        [MinLength(11, ErrorMessage = "TC must be 11 characters.")]
        [MaxLength(11, ErrorMessage = "TC can be 11 characters.")]
        public string id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name can be max 30 characters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3, ErrorMessage = "Password can be min 3 characters.")]
        [MaxLength(16, ErrorMessage = "Password can be max 16 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Password is required.")]
        [MinLength(3, ErrorMessage = "Password can be min 3 characters.")]
        [MaxLength(16, ErrorMessage = "Password can be max 16 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "PoliclinicId is required.")]
        public long  PoliclinicId { get; set; }





    }
}
