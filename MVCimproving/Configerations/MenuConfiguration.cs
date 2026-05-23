using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCimproving.Models;

namespace MVCimproving.Configerations
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(1000);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.Property(e => e.IsAvailable).HasDefaultValue(true);
        }
    }
}
