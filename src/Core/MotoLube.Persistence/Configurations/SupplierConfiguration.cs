using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable(TableNames.Suppliers);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(100);

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(s => s.Address)
            .HasMaxLength(150);
    }
}
