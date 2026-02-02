using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Common;

namespace MotoLube.Persistence.Configurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableNames.Customers);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasMaxLength(70);
        
        builder.Property(c => c.LastName)
            .HasMaxLength(70);
        
        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(15);
    }
}