using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Specializations;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Infrastructure.Staffs;
using DDDSample1.Infrastructure.Specializations;


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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureSpecialization(modelBuilder);  // Call to configure Specialization entity
            ConfigurePatient(modelBuilder);         // Call to configure Patient entity
            ConfigureStaff(modelBuilder);           // Call to configure Staff entity
            
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
            /*
            modelBuilder.ApplyConfiguration(new SpecializationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            */
            
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

            
        }

    }
}