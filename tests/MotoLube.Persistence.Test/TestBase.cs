using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test;

[Collection(nameof(DatabaseCollection))]
public abstract class TestBase(DatabaseFixture fixture, ITestOutputHelper outputHelper)
{
    protected ITestOutputHelper OutputHelper { get; } = outputHelper;
    protected AppDbContext Context { get; } = fixture.Context;

    protected void ExecuteInATransaction(Action action)
    {
        IExecutionStrategy strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(state: action, (operation) =>
        {
            using IDbContextTransaction transaction = Context.Database.BeginTransaction();
            operation();
            transaction.Rollback();

        });
    }

    protected Task ExecuteInATransactionAsync(Func<Task> action)
    {
        IExecutionStrategy strategy = Context.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(state: action, async (operation) =>
        {
            await using IDbContextTransaction transaction = await Context.Database.BeginTransactionAsync();
            await operation();
            await transaction.RollbackAsync();
        });
    }
}
