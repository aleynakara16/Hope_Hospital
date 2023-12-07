using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Entities
{
    public class Admin
    {

        [Key]
        public Guid Admin_Id { get; set; } //benzersin olması için guid kullanıldı

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Admin_mail { get; set; }

        [Required]
        [StringLength(100)]
        public string Admin_Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "admin";
    }
}
