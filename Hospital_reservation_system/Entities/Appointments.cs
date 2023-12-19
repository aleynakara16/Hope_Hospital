using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Entities
{
    [Table("Appointments")]// veritabanındaki tablo ismi
    public class Appointments
    {
        [Key]
        public Guid AppointmentID { get; set; }

        [Required]
        public String UserID { get; set; }
        public User User { get; set; }

        [Required] 
        public String DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public string Policlinicname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[MyAppointmentDateValidation(ErrorMessage = "Are you creating an appointment for the past?")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        public int CompareTo(Appointments other)
        {
            return this.Date.Date.Add(this.Time.TimeOfDay).CompareTo(other.Date.Date.Add(other.Time.TimeOfDay));
        }
    }
}