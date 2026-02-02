using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100);
    }
}