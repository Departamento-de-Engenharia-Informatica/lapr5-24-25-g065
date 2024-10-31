using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Infrastructure.Staffs
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs"); // Use appropriate table name

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Firstname).IsRequired();
            builder.Property(b => b.LastName).IsRequired();
            builder.Property(b => b.FullName).IsRequired();
            builder.Property(b => b.Gender).IsRequired();
            builder.Property(b => b.Specialization).IsRequired();
            builder.Property(b => b.Type).IsRequired();
            builder.Property(b => b.LicenseNumber).IsRequired();
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.AvailabilitySlot).IsRequired();
            builder.Property(b => b.PhoneNumber).IsRequired();
            builder.Property(b => b.Email).IsRequired();
        }
    }
}
