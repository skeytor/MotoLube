using MotoLube.Application.Abstractions.Messaging;
using MotoLube.Application.DTOs.Requests;
using MotoLube.Application.Extensions.Mappers;
using MotoLube.Domain.Repositories;
using SharedKernel.Pagination;
using SharedKernel.Results;

namespace MotoLube.Application.UseCases.Products.Get;

public sealed record GetProductsQuery(PagingParameters Paging, PagingFilters Filters) : IQuery<PagedList<ProductResponse>>;

internal sealed class GetProductsQueryHandler(IProductRepository repo) : IQueryHandler<GetProductsQuery, PagedList<ProductResponse>>
{
    public async Task<Result<PagedList<ProductResponse>>> HandleAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        (int page, int size) = query.Paging;

        var products = await repo.GetPagedListAsync(
            page,
            size,
            query.Filters,
            selector: p => p.ToResponse(),
            cancellationToken);

        int totalCount = await repo.CountAsync();

        return PagedList<ProductResponse>.Create(products, page, size, totalCount);
    }
}