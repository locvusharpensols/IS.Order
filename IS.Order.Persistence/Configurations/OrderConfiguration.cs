using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IS.Order.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.Property(o => o.FundId).IsRequired();
        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.Amount).IsRequired().HasConversion<decimal>();
        builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50).HasConversion<string>();
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.Property(o => o.CustomerId).IsRequired().HasMaxLength(50).HasConversion<string>();
        builder.HasIndex(o => o.CustomerId);
    }
}