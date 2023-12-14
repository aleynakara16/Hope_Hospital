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
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Doctor ve Appointment arasında One-to-Many ilişkisi
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.DoctorAppointmens)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorID);

            // Polyclinic ve Doctor arasında One-to-Many ilişkisi
            modelBuilder.Entity<Policlinic>()
                .HasMany(p => p.Doctors)
                .WithOne(d => d.Policlinic)
                .HasForeignKey(d => d.PoliclinicID);


            // Appointment ve user arasında Many-to-One ilişkisi
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User) // bir randevu bir hastaya aittir.
                .WithMany(u => u.Appointments) // bir hastanın birden fazla randevusu olabilir.
                .HasForeignKey(a => a.UserID);


            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever(); // ID'nin otomatik oluşturulmasını engelle
           
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Id)
                .ValueGeneratedNever(); // ID'nin otomatik oluşturulmasını engelle


            modelBuilder.Entity<Admin>()
                .Property(a =>a.Admin_Id )
                .ValueGeneratedNever(); // ID'nin otomatik oluşturulmasını engelle
        }

    }
}