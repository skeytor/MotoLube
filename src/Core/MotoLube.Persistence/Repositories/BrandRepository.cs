using Microsoft.EntityFrameworkCore;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;

namespace MotoLube.Persistence.Repositories;

internal sealed class BrandRepository(AppDbContext context) 
    : Repository<Brand, int>(context), IBrandRepository
{
    public Task<Brand?> FindByNameAsync(string name) => 
        Context.Brands.FirstOrDefaultAsync(b => b.Name == name);
}