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
                .IsRequired() // Make required if necessary
                .HasMaxLength(100); // Set maximum length

            builder.Property(b => b.LastName)
                .IsRequired() // Make required if necessary
                .HasMaxLength(100); // Set maximum length

            builder.Property(b => b.FullName)
                .IsRequired() // Make required if necessary
                .HasMaxLength(200); // Set maximum length

            builder.Property(b => b.Gender)
                .IsRequired() // Make required if necessary
                .HasMaxLength(10); // Adjust according to your needs

            builder.Property(b => b.EmergencyContact)
                .IsRequired() // Make required if necessary
                .HasMaxLength(100); // Set maximum length

            builder.Property(b => b.DateOfBirth)
                .IsRequired(false); // Nullable field

            builder.Property(b => b.MedicalRecordNumber)
                .IsRequired() // Make required if necessary
                .HasMaxLength(50); // Set maximum length

            builder.Property(b => b.UserId)
                .IsRequired(); // Ensure UserId is required

            // Configure relationships if necessary
            builder.HasMany(b => b.AppointmentHistory)
                .WithOne() // Assuming there's a reference back to Patient in Appointment
                .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior if necessary

            // Additional configurations can go here
        }
    }
}
