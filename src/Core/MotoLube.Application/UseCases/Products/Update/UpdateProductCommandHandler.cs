using MotoLube.Application.Abstractions.Messaging;
using MotoLube.Application.DTOs.Requests;
using MotoLube.Application.Extensions.Mappers;
using MotoLube.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace MotoLube.Application.UseCases.Products.Update;


public sealed record UpdateProductCommand(Guid ProductId, UpdateProductRequest Request) : ICommand;

internal class UpdateProductCommandHandler(IProductRepository repo, IUnitOfWork unit) 
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await repo.FindByIdAsync(command.ProductId);

        if (product is null)
        {
            return Result.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product with ID '{command.ProductId}' not found."));
        }

        if (!await repo.IsSkuUniqueAsync(command.Request.Sku, excludeId: command.ProductId))
        {
            return Result.Failure(Error.Conflict(
                "Product.SkuConflict",
                $"SKU '{command.Request.Sku}' is already in use."));
        }

        command.Request.ApplyTo(product);

        await unit.SaveChangesAsync();

        return Result.Success();
    }
}