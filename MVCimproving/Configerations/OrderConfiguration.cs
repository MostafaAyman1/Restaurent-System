using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCimproving.Models;

namespace MVCimproving.Configerations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Status).HasDefaultValue("Pending").HasMaxLength(50);
            builder.HasMany(e => e.OrderItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
        }
    }
}
