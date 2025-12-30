using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class OutboundConfiguration : IEntityTypeConfiguration<Outbound>
{
    public void Configure(EntityTypeBuilder<Outbound> builder)
    {
        builder.ToTable(TableNames.Outbounds);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedAt)
            .ValueGeneratedOnUpdate();

        ConfigureRelationships(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Outbound> builder)
    {
        // One-to-Many relationship between Outbound and Customer
        builder.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        // Many-to-Many relationship between Outbound and Product through OutboundItem
        builder.HasMany<Product>()
            .WithMany()
            .UsingEntity<OutboundItem>();
    }
}
