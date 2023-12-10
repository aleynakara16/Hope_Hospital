using Hospital_reservation_system.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hospital_reservation_system.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Policlinic> Policlinics { get; set; }
        public DbSet<Department> Departments { get; set; }



    }
}