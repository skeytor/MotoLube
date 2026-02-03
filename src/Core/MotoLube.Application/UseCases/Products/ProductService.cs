using MotoLube.Application.DTOs.Requests;
using MotoLube.Application.Extensions.Mappers;
using MotoLube.Domain.Entities;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace MotoLube.Application.UseCases.Products;

public sealed class ProductService(IProductRepository repository, IUnitOfWork unit) : IProductService
{
    public async Task<Result<Guid>> CreateAsync(CreateProductRequest request)
    {
        if (!await repository.IsSkuUniqueAsync(request.Sku))
        {
            return Result.Failure<Guid>(Error.Conflict(
                "Product.DuplicateSku",
                $"SKU '{request.Sku}' already exists."));
        }

        Product product = request.ToEntity();

        await repository.InsertAsync(product);
        await unit.SaveChangesAsync();

        return product.Id;
    }

    public async Task<Result<ProductResponse>> GetByIdAsync(Guid id)
    {
        Product? product = await repository.FindByIdAsync(id);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(Error.NotFound(
                "Product.NotFound",
                $"Product with ID '{id}' was not found."));
        }

        return product.ToResponse();
    }

    public async Task<Result<IReadOnlyCollection<ProductResponse>>> GetPagedAsync(
        PaginationOptions pagingOptions,
        PaginationQueryFilters queryFilters)
    {
        IReadOnlyCollection<ProductResponse> products = await repository.GetPagedAsync(
            pagingOptions,
            queryFilters,
            selector: s => s.ToResponse());

        return Result.Success(products);
    }
}
