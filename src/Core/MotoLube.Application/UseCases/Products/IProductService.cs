using MotoLube.Application.DTOs.Requests;
using SharedKernel.Pagination;
using SharedKernel.Results;

namespace MotoLube.Application.UseCases.Products;

public interface IProductService
{
    Task<Result<Guid>> CreateAsync(CreateProductRequest request);
    Task<Result<ProductResponse>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyCollection<ProductResponse>>> GetPagedAsync(
        PaginationOptions pagingOptions, 
        PaginationQueryFilters queryFilters);
}
