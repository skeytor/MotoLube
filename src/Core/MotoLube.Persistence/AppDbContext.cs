using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Persistence.Abstractions;

namespace MotoLube.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options), IAppDbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Brand> Brands => Set<Brand>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Inbound> Inbounds => Set<Inbound>();

    public DbSet<InboundItem> InboundItems => Set<InboundItem>();

    public DbSet<Outbound> Outbounds => Set<Outbound>();

    public DbSet<OutboundItem> OutboundItems => Set<OutboundItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
