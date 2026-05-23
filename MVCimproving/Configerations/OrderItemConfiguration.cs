using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCimproving.Models;

namespace MVCimproving.Configerations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Quantity).IsRequired();
            builder.HasOne(e => e.MenuItem).WithMany().HasForeignKey(e => e.MenuItemId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
