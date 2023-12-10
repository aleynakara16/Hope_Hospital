using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hospital_reservation_system.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public String Department_Id { get; set; }

        [Required]
        public string Department_Name { get; set; }
    }
}
