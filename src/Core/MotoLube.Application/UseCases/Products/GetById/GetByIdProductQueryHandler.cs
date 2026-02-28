using MotoLube.Application.Abstractions.Messaging;
using MotoLube.Application.DTOs.Requests;
using MotoLube.Application.Extensions.Mappers;
using MotoLube.Domain.Repositories;
using SharedKernel.Results;

namespace MotoLube.Application.UseCases.Products.GetById;

public sealed record GetByIdProductQuery(Guid ProductId) : IQuery<ProductResponse>;

internal sealed class GetByIdProductQueryHandler(IProductRepository repo) 
    : IQueryHandler<GetByIdProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(GetByIdProductQuery query, CancellationToken cancellationToken)
    {
        var product = await repo.FindByIdAsync(query.ProductId);

        if (product is null || !product.IsActive)
        {
            return Result.Failure<ProductResponse>(Error.NotFound(
                "Product.NotFound",
                $"Product with ID '{query.ProductId}' was not found."));
        }

        return product.ToResponse();
    }
}