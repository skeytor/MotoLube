using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(150);

        builder.Property(p => p.Sku)
            .HasMaxLength(20);

        builder.Property(p => p.Price)
            .HasPrecision(14, 2);

        builder.Property(p => p.Tax)
            .HasPrecision(5, 2);

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.HasIndex(p => p.Sku)
            .IsUnique();

        builder.HasIndex(p => p.Name);

        ConfigureRelationships(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Product> builder)
    {
        // Many-to-One relationship between Product and Category
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        // Many-to-One relationship between Product and Brand
        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);
    }
}
