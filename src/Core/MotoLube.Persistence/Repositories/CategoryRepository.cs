using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;

namespace MotoLube.Persistence.Repositories;

internal sealed class CategoryRepository(AppDbContext context)
    : Repository<Category, int>(context), ICategoryRepository;