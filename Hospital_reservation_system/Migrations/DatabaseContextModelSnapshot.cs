﻿// <auto-generated />
using System;
using Hospital_reservation_system.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hospital_reservation_system.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hospital_reservation_system.Entities.Admin", b =>
                {
                    b.Property<long>("Admin_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Admin_Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Admin_mail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Admin_Id");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Appointments", b =>
                {
                    b.Property<Guid>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Policlinicname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AppointmentID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("UserID");

                    b.ToTable("Appointments", (string)null);
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Doctor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("PoliclinicID")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("PoliclinicID");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Policlinic", b =>
                {
                    b.Property<long>("Policlinic_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Policlinic_Id"));

                    b.Property<string>("Policlinic_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Policlinic_Id");

                    b.ToTable("Policlinics", (string)null);
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Appointments", b =>
                {
                    b.HasOne("Hospital_reservation_system.Entities.Doctor", "Doctor")
                        .WithMany("DoctorAppointmens")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_reservation_system.Entities.User", "User")
                        .WithMany("AppointmentList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Doctor", b =>
                {
                    b.HasOne("Hospital_reservation_system.Entities.Policlinic", "Policlinic")
                        .WithMany("Doctors")
                        .HasForeignKey("PoliclinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policlinic");
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorAppointmens");
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.Policlinic", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Hospital_reservation_system.Entities.User", b =>
                {
                    b.Navigation("AppointmentList");
                });
#pragma warning restore 612, 618
        }
    }
}
