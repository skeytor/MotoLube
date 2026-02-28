using MotoLube.Application.Abstractions.Messaging;
using MotoLube.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace MotoLube.Application.UseCases.Products.UpdateStock;


public sealed record UpdateProductStockCommand(Guid ProductId, int Quantity, UpdateStockOperation Operation) : ICommand;

internal sealed class UpdateProductStockCommandHandler(IProductRepository repo, IUnitOfWork unit) : ICommandHandler<UpdateProductStockCommand>
{
    public async Task<Result> HandleAsync(UpdateProductStockCommand command, CancellationToken cancellationToken)
    {
        if (command.Quantity < 0)
        {
            return Result.Failure(Error.Validation(
                "Product.InvalidQuantity",
                "Quantity must be a non-negative integer."));
        }

        var product = await repo.FindByIdAsync(command.ProductId);

        if (product is null)
        {
            return Result.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product with ID '{command.ProductId}' not found."));
        }
        
        if (command.Operation is UpdateStockOperation.Decrease &&
            command.Quantity > product.Stock)
        {
            return Result.Failure(Error.Validation(
                "Product.InvalidStock",
                "Insufficient stock to decrease."));
        }

        int updatedStock = command.Operation switch
        {
            UpdateStockOperation.Set => command.Quantity,
            UpdateStockOperation.Increase => product.Stock + command.Quantity,
            UpdateStockOperation.Decrease => product.Stock - command.Quantity,
            _ => product.Stock
        };

        product.Stock = updatedStock;
        await unit.SaveChangesAsync();
        return Result.Success();
    }
}

public enum UpdateStockOperation
{
    Set,
    Increase,
    Decrease
}