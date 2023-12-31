﻿using Hospital_reservation_system.Entities;
using Hospital_reservation_system.validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_reservation_system.Models
{
    public class AppointmentViewModel
    {
        public Guid appointment_id { get; set; }

        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız!")]
        public String currentUserID { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public String selecktedDoctorID { get; set; }

        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string policlinicID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter a valid date.")]
        [FutureDate(ErrorMessage = "Please select a future date.")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please enter a valid time.")]
        public DateTime Time { get; set; }

        // public List<Appointments> Appointments { get; set; }
    }
}