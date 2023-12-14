using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_reservation_system.Entities
{
    [Table("Admins")]// veritabanındaki tablo ismi

    public class Admin
    {
        [Key]
        public long Admin_Id { get; set; } 

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
