using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace Hospital_reservation_system.Entities
{
    [Table("Policlinics")]
    public class Policlinic
    {
        [Key]
        public long Policlinic_Id { get; set; }

        [Required]
        public string Policlinic_Name { get; set; }
        
        public List<Doctor> Doctors { get; set; }

    }
}