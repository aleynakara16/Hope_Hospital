using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Hospital_reservation_system.Entities
{
    [Table("Doctors")]// veritabanındaki tablo ismi
    public class Doctor
    {
        [Key]
        public String Id { get; set; } //benzersin olması için guid kullanıldı

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }


        public bool Locked { get; set; } = false;// kullanıcıyı silmek yerine pasife almak için kullanıcaz
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "doctor";
       
        [Required]
        public long PoliclinicID { get; set; }
        public Policlinic Policlinic { get; set; }


        public List<Appointments> DoctorAppointmens { get; set; }
    }
}