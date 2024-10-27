using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Infrastructure.Staffs;
using DDDSample1.Infrastructure.Specializations;
using DDDSample1.Infrastructure.OperationTypes;
using DDDSample1.Infrastructure.Passwords;

using System;
using DDDNetCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Text.Json;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        
    

        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationType> OperationType {get;set;}
        public DbSet<Password> Passwords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureSpecialization(modelBuilder);  // Call to configure Specialization entity
            ConfigurePatient(modelBuilder);         // Call to configure Patient entity
            ConfigureStaff(modelBuilder);           // Call to configure Staff entity
            ConfigureUser(modelBuilder);
            ConfigureOperationsType(modelBuilder);
            ConfigurePassword(modelBuilder);
        }

        private void ConfigureOperationsType(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<OperationType>()
                .HasKey(ot => ot.Id);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new OperationTypeID(v)
                )
                .HasColumnName("OperationTypeId");

            var valueConverter = new ValueConverter<List<Specialization>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),   // Convert list to JSON string for storage
                v => JsonSerializer.Deserialize<List<Specialization>>(v, (JsonSerializerOptions)null) // Convert JSON string back to list
            );

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.RequiredStaffBySpecialization)
                .HasConversion(valueConverter);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.EstimatedDuration)
                .HasMaxLength(500);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.IsActive);
        }

        private void ConfigureSpecialization(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Specialization>()
                .Property(s => s.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new SpecializationId(v)
                )
                .HasColumnName("SpecializationId");

            modelBuilder.Entity<Specialization>()
                .Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Specialization>()
                .Property(s => s.Description)
                .HasMaxLength(500);
        }

        private void ConfigurePatient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new PatientId(v)
                )
                .HasColumnName("PatientId");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Firstname)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Patient>()
                .Property(p => p.EmergencyContact)
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DateOfBirth)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            // Handle the Allergies list as a comma-separated string
            modelBuilder.Entity<Patient>()
                .Property(p => p.Allergies)
                .IsRequired()
                .HasMaxLength(500); // Adjust the max length based on your needs
        }

        private void ConfigureStaff(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Staff>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new StaffId(v)
                )
                .HasColumnName("StaffId");

            modelBuilder.Entity<Staff>()
                .Property(s => s.Firstname)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Gender)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Staff>()
                .Property(s => s.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Specialization as a reference (navigation property)
            modelBuilder.Entity<Staff>()
            .HasOne(s => s.Specialization) // Navigation property
            .WithMany(sp => sp.StaffMembers) // Specialization can have many Staff members
            .HasForeignKey(s => s.SpecializationId); // Foreign key

            modelBuilder.Entity<Staff>()
            .HasOne(s => s.User) // Each Staff has a required User
            .WithOne() // No navigation property on User (because User doesn't reference Staff)
            .HasForeignKey<Staff>(s => s.UserId) // Foreign key in Staff
            .IsRequired(); // Staff must have a User


            
        }

        private void ConfigureUser(ModelBuilder modelBuilder){
             modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new UserId(v)
                )
                .HasColumnName("UserId");

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion(
                v => v.ToString(),        // Convert enum to string for storage
                v => (Role)Enum.Parse(typeof(Role), v) // Convert string back to enum
            )
            .IsRequired()
            .HasMaxLength(50); // Adjust max length based on your enum values
        }

        public void ConfigurePassword(ModelBuilder modelBuilder){
             modelBuilder.Entity<Password>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Password>()
                .Property(p => p.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new PasswordId(v)
                )
                .HasColumnName("PasswordId");
            
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();
        }

    }
}