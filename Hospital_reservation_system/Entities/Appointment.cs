using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Entities
{
    [Table("Appointment")]// veritabanındaki tablo ismi
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required] 
        public string DoctorID { get; set; }

        [Required]
        [Display(Name = "Date for Appointment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[MyAppointmentDateValidation(ErrorMessage = "Are you creating an appointment for the past?")]
        public DateTime Date { get; set; }

        //Disabling due to variable appointment times now. 
        //[MyTimeValidation(ErrorMessage="Appointments only available for HH:00 and HH:30")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string TimeBlockHelper { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual User User { get; set; }

        public int CompareTo(Appointment other)
        {
            return this.Date.Date.Add(this.Time.TimeOfDay).CompareTo(other.Date.Date.Add(other.Time.TimeOfDay));
        }
    }
}