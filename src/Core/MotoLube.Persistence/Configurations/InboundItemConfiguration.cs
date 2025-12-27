using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class InboundItemConfiguration : IEntityTypeConfiguration<InboundItem>
{
    public void Configure(EntityTypeBuilder<InboundItem> builder)
    {
        builder.ToTable(TableNames.InboundItems);

        builder.HasKey(i => new { i.InboundId, i.ProductId });
        
        builder.Property(i => i.UnitPrice)
            .HasPrecision(14, 2);
    }
}
