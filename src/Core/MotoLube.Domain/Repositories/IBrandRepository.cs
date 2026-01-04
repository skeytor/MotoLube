using MotoLube.Domain.Entities;

namespace MotoLube.Domain.Repositories;

public interface IBrandRepository : IRepository<int, Brand>
{
    Task<Brand?> FindByNameAsync(string name);
}
