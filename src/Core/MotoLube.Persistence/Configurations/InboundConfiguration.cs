using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class InboundConfiguration : IEntityTypeConfiguration<Inbound>
{
    public void Configure(EntityTypeBuilder<Inbound> builder)
    {
        builder.ToTable(TableNames.Inbounds);

        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(i => i.UpdatedAt)
            .ValueGeneratedOnUpdate();

        ConfigureRelationships(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Inbound> builder)
    {
        // One-to-Many relationship between Inbound and Supplier
        builder.HasOne(i => i.Supplier)
            .WithMany()
            .HasForeignKey(i => i.SupplierId);

        // Many-to-Many relationship between Inbound and Product through InboundItem
        builder.HasMany<Product>()
            .WithMany()
            .UsingEntity<InboundItem>();
    }
}