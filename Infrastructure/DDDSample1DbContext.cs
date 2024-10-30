using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Text.Json;
using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.OperationType;
using DDDNetCore.Domain.OperationRequestDomain;
using DDDNetCore.Domain;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DDDSample1DbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<OperationRequest> OperationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureSpecialization(modelBuilder);
            ConfigurePatient(modelBuilder);
            ConfigureStaff(modelBuilder);
            ConfigureUser(modelBuilder);
            ConfigureOperationType(modelBuilder);
            ConfigureOperationRequest(modelBuilder);
            ConfigureAppointment(modelBuilder);
            // Uncomment if needed
            // ConfigurePassword(modelBuilder);
        }

        private void ConfigureOperationRequest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationRequest>()
                .HasKey(or => or.Id);

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new OperationRequestID(v)
                )
                .HasColumnName("OperationRequestId");

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.patientID)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.doctorID)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.operationTypeID)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.operationDateTime)
                .IsRequired();

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.deadline)
                .IsRequired();

            modelBuilder.Entity<OperationRequest>()
                .Property(or => or.priority)
                .IsRequired();

            // Relationships (if applicable, adjust if foreign key needs clarification)
            modelBuilder.Entity<OperationRequest>()
                .HasOne<Patient>() // Each OperationRequest is associated with a Patient
                .WithMany() // Assuming there's no specific navigation back in Patient
                .HasForeignKey(or => or.patientID);

            modelBuilder.Entity<OperationRequest>()
                .HasOne<Staff>() // Each OperationRequest has an assigned doctor
                .WithMany() // Assuming there's no specific navigation back in Staff
                .HasForeignKey(or => or.doctorID);

            modelBuilder.Entity<OperationRequest>()
                .HasOne<OperationType>() // Each OperationRequest has one OperationType
                .WithMany() // Assuming there's no specific navigation back in OperationType
                .HasForeignKey(or => or.operationTypeID);
        }


        private void ConfigureAppointment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new AppointmentId(v)
                )
                .HasColumnName("AppointmentId");

            modelBuilder.Entity<Appointment>()
                .Property(a => a.RequestId)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.RoomId)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Date)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            modelBuilder.Entity<Appointment>()
                .HasOne<Patient>() // Each Appointment is linked to one Patient
                .WithMany(p => p.AppointmentHistory) // A Patient can have many Appointments
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne<Staff>() // Each Appointment is handled by one Staff member
                .WithMany(s => s.Appointments) // A Staff member can handle many Appointments
                .HasForeignKey(a => a.StaffId);
        }

        private void ConfigureOperationType(ModelBuilder modelBuilder)
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

            var valueConverter = new ValueConverter<List<Specialization>, string>( // Adjust based on your needs
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<Specialization>>(v, (JsonSerializerOptions)null)
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
                .Property(p => p.PhoneNumber) // Configure PhoneNumber property
                .IsRequired() // Make required if necessary
                .HasMaxLength(15); // Set maximum length (adjust based on your phone number format)

            modelBuilder.Entity<Patient>()
                .Property(p => p.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            var valueConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<Patient>()
                .Property(p => p.Allergies)
                .HasConversion(valueConverter);

            // Add the UserId property mapping
            modelBuilder.Entity<Patient>()
                .Property(p => p.UserId)
                .IsRequired(); // Ensure UserId is required
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

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Specialization)
                .WithMany(sp => sp.StaffMembers)
                .HasForeignKey(s => s.SpecializationId);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Staff>(s => s.UserId)
                .IsRequired();
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
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
                    v => v.ToString(),
                    v => (Role)Enum.Parse(typeof(Role), v)
                )
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().OwnsOne(u => u.Password);
        }

        /*
        public void ConfigurePassword(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Password>()
                .Property(p => p.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new PasswordId(v)
                )
                .HasColumnName("PasswordId");
            
            modelBuilder.Entity<Password>()
                .Property(p => p.Pass)
                .IsRequired();
        }
        */
    }
}
