using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IBrandRepository : IRepository<Brand, int>
{
    Task<Brand?> FindByNameAsync(string name);
}