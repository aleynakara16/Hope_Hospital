using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Models
{
    public class AdminViewModel
    {
        public string Admin_Id { get; set; } //benzersin olması için guid kullanıldı

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Admin_mail { get; set; }

        [Required]
        [StringLength(100)]
        public string Admin_Password { get; set; }

    }
}
