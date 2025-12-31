using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test;

[Collection(nameof(DatabaseCollection))]
public abstract class TestBase(DatabaseFixture fixture, ITestOutputHelper outputHelper)
{
    protected readonly ITestOutputHelper _outputHelper = outputHelper;
    protected readonly AppDbContext _context = fixture.Context;

    protected void ExecuteInATransaction(Action action)
    {
        IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();
        strategy.Execute(state: action, (operation) =>
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            operation();
            transaction.Rollback();

        });
    }
    protected Task ExecuteInATransactionAsync(Func<Task> action)
    {
        IExecutionStrategy strategy = _context.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(state: action, async (operation) =>
        {
            await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            await operation();
            await transaction.RollbackAsync();
        });
    }
}
