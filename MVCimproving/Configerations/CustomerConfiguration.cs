using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCimproving.Models;

namespace MVCimproving.Configerations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(50);
            builder.Property(e => e.Address).HasMaxLength(500);
            builder.Property(e => e.Email).HasMaxLength(200);
        }
    }
}
