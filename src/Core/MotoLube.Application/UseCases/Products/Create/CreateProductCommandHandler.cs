using MotoLube.Application.Abstractions.Messaging;
using MotoLube.Application.Extensions.Mappers;
using MotoLube.Application.DTOs.Requests;
using MotoLube.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace MotoLube.Application.UseCases.Products.Create;

public sealed record CreateProductCommand(CreateProductRequest Request) : ICommand<Guid>;

internal sealed class CreateProductCommandHandler(IProductRepository repo, IUnitOfWork unit) 
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (!await repo.IsSkuUniqueAsync(command.Request.Sku))
        {
            return Result.Failure<Guid>(Error.Conflict(
                "Product.DuplicateSku",
                $"SKU '{command.Request.Sku}' already exists."));
        }

        var product = command.Request.ToEntity();
        await repo.InsertAsync(product);
        await unit.SaveChangesAsync();

        return product.Id;
    }
}