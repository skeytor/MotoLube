using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class OutboundItemConfiguration : IEntityTypeConfiguration<OutboundItem>
{
    public void Configure(EntityTypeBuilder<OutboundItem> builder)
    {
        builder.ToTable(TableNames.OutboundItems);

        builder.HasKey(o => new { o.OutboundId, o.ProductId });

        builder.Property(o => o.UnitPrice)
            .HasPrecision(14, 2);
    }
}