using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.Passwords;
using DDDNetCore.Domain.OperationRequestDomain;
using DDDSample1.Domain.OperationType; // Add the OperationType namespace
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DDDSample1DbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<OperationRequest> OperationRequests { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; } // Add DbSet for OperationType

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePatient(modelBuilder);
            ConfigureStaff(modelBuilder); // Configure Staff
            ConfigureOperationType(modelBuilder); // Configure OperationType
            ConfigureOperationRequest(modelBuilder);
            ConfigureAppointment(modelBuilder);
        }

        private void ConfigureStaff(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .HasKey(s => s.Id); // Assuming Id is the key inherited from Entity

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
                .Property(s => s.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Staff>()
                .Property(s => s.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Staff>()
                .Property(s => s.AvailabilitySlot)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Staff>()
                .Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(250);

            // Relationships
            modelBuilder.Entity<Staff>()
                .HasOne<User>()
                .WithMany() // Assuming one-to-many relation with User
                .HasForeignKey(s => s.UserId); // Ensure the relationship with User is set up

            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Appointments)
                .WithOne(a => a.Staff) // Ensure Appointment has a navigation property for Staff
                .HasForeignKey(a => a.StaffId);
        }

        private void ConfigureOperationType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationType>()
                .HasKey(ot => ot.Id); // Assuming Id is the key inherited from Entity

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new OperationTypeID(v)
                )
                .HasColumnName("OperationTypeId");

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.EstimatedDuration)
                .IsRequired();

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.IsActive)
                .IsRequired();

            var valueConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.RequiredStaffBySpecialization)
                .HasConversion(valueConverter); // Serialize List<string> to JSON string
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

            // Relationships
            modelBuilder.Entity<OperationRequest>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(or => or.patientID);

            modelBuilder.Entity<OperationRequest>()
                .HasOne<Staff>()
                .WithMany()
                .HasForeignKey(or => or.doctorID);
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
                .HasOne<Patient>()
                .WithMany(p => p.AppointmentHistory)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne<Staff>()
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StaffId);
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
                .Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);

            var valueConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalConditions)
                .HasConversion(valueConverter); // Serialize List<string> to JSON string

            // Relationships
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.AppointmentHistory)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);
        }
    }
}
