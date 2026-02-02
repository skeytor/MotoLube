using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;

namespace MotoLube.Persistence.Repositories;

internal sealed class SupplierRepository(AppDbContext context)
    : Repository<Supplier, Guid>(context), ISupplierRepository
{
    public Task<Supplier?> FindByNameAsync(string name) =>
        Context.Suppliers.FirstOrDefaultAsync(s => s.Name == name);

    public async Task<bool> IsNameUniqueAsync(string name) =>
        !await Context.Suppliers.AnyAsync(s => s.Name == name);
}