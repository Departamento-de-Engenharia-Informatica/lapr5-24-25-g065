using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Patients;

namespace DDDSample1.Infrastructure.Patients
{
    internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            // Specify the table name if necessary
            // builder.ToTable("Patients", SchemaNames.DDDSample1);

            // Set the primary key
            builder.HasKey(b => b.Id);

            // Configure properties
            builder.Property(b => b.Firstname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(b => b.EmergencyContact)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(b => b.DateOfBirth)
                .IsRequired(false);

            builder.Property(b => b.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.Property(b => b.Email) // Configure Email property
                .IsRequired() // Set required if necessary
                .HasMaxLength(150); // Set max length based on application needs

            // Configure relationships if necessary
            builder.HasMany(b => b.AppointmentHistory)
                .WithOne() // Assuming there's a reference back to Patient in Appointment
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
