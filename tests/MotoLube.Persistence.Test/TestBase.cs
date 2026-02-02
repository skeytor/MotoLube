using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MotoLube.Persistence.Test.Fixtures;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test;

/// <summary>
/// Provides a base class for database integration tests, supplying access to the test database context and output
/// helper for logging.
/// </summary>
/// <remarks>This class is intended to be inherited by test classes that require access to a shared database
/// context and output logging. It ensures consistent setup and teardown of database resources across tests.</remarks>
/// <param name="fixture">The database fixture that provides the test database context and manages its lifecycle.</param>
/// <param name="outputHelper">The output helper used to capture and display test output during execution.</param>
[Collection(nameof(DatabaseCollection))]
public abstract class TestBase(DatabaseFixture fixture, ITestOutputHelper outputHelper)
{
    protected ITestOutputHelper OutputHelper { get; } = outputHelper;

    /// <summary>
    /// Gets the database context used for data access operations within the test fixture.
    /// </summary>
    /// <remarks>This property provides access to the underlying <see cref="AppDbContext"/> instance
    /// configured for the test environment. Use this context to query or modify test data as part of integration
    /// tests.</remarks>
    protected AppDbContext Context { get; } = fixture.Context;

    /// <summary>
    /// Executes the specified action within a database transaction using the current execution strategy.
    /// </summary>
    /// <remarks>The transaction is always rolled back after the action completes, so no changes are committed
    /// to the database. This method is useful for testing transactional behavior or executing code that should not
    /// persist changes.</remarks>
    /// <param name="action">The action to execute within the transaction. Cannot be null.</param>
    protected void ExecuteInTransaction(Action action)
    {
        IExecutionStrategy strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(state: action, (operation) =>
        {
            using IDbContextTransaction transaction = Context.Database.BeginTransaction();
            operation();
            transaction.Rollback();

        });
    }

    /// <summary>
    /// Executes the specified asynchronous operation within a database transaction using the current execution
    /// strategy.
    /// </summary>
    /// <remarks>The transaction is rolled back after the operation completes, regardless of success or
    /// failure. This method is useful for testing transactional behavior without persisting changes to the
    /// database.</remarks>
    /// <param name="action">A delegate representing the asynchronous operation to execute within the transaction. The operation should not
    /// commit or roll back the transaction.</param>
    /// <returns>A task that represents the asynchronous execution of the operation within a transaction.</returns>
    protected Task ExecuteInTransactionAsync(Func<Task> action)
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