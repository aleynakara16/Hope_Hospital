﻿using Hospital_reservation_system.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_reservation_system.Models
{
    public class AppointmentViewModel
    {
        [Display(Name = "Şehirler")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız!")]
        public string city { get; set; }

        [Display(Name = "Hastaneler")]
        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string hospitalName { get; set; }


        [Display(Name = "Poliklinikler")]
        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string policlinicName { get; set; }


        public int DoctorId { get; set; }

        [Display(Name = "Doktorlar")]
        [Required(ErrorMessage = "Bu Alanı Boş Bırakamazsınız!")]
        public string DoctorName { get; set; }

    }
}
