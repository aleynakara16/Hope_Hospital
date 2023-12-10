using Hospital_reservation_system.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_reservation_system.Models
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız!")]
        public string currentUserID { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string selecktedDoctorID { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string selectedDoctorName { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string policlinicName { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string departmentName { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public DateTime dateTime { get; set; }
    }
}
