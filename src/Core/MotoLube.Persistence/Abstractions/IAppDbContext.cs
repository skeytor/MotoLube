using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;

namespace MotoLube.Persistence.Abstractions;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<Brand> Brands { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Inbound> Inbounds { get; }
    DbSet<InboundItem> InboundItems { get; }
    DbSet<Outbound> Outbounds { get; }
    DbSet<OutboundItem> OutboundItems { get; }
}
