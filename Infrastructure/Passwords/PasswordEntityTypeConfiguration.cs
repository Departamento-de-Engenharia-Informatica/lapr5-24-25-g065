/*
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Passwords;

namespace DDDSample1.Infrastructure.Passwords
{
    internal class PasswordEntityTypeConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            //builder.ToTable("Products", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
        }
    }
}
*/