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

    }
}