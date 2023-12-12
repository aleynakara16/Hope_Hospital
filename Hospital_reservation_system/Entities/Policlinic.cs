using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hospital_reservation_system.Entities
{
    [Table("Policlinics")]
    public class Policlinic
    {
        [Key]
        public String Policlinic_Id { get; set; }

        [Required]
        public string Policlinic_Name { get; set; }

    }
}