using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoLube.Persistence.Test.DataInitializers;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace MotoLube.Persistence.Test.Fixtures;

/// <summary>
/// Provides a test fixture that manages the lifecycle of a PostgreSQL container and an associated application database
/// context for integration testing.
/// </summary>
/// <remarks>Use this fixture to ensure a consistent, isolated database environment for tests that require
/// database access. The fixture initializes a PostgreSQL container and creates the database schema before tests run,
/// and disposes of resources after tests complete. This type is intended for use with test frameworks that support
/// asynchronous lifecycle management via the IAsyncLifetime interface.</remarks>
public sealed class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer? _container;

    public AppDbContext Context { get; private set; } = null!;
    public string ConnectionString => 
        _container?.GetConnectionString() ?? 
        throw new InvalidOperationException("Container not initialized.");

    public Task DisposeAsync() => _container!.DisposeAsync().AsTask();

    /// <summary>
    /// Initializes the database container and ensures that the database is created asynchronously.
    /// </summary>
    /// <remarks>This method starts a PostgreSQL container, creates a database context, and ensures that the
    /// database schema is created. It should be called before performing any database operations that depend on the
    /// initialized context.</remarks>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder("postgres:latest").Build();
        await _container.StartAsync();
        Context = GetDbContext();
        await Context.Database.EnsureCreatedAsync();
    }

    /// <summary>
    /// Creates and configures a new instance of the application's database context for use with PostgreSQL.
    /// </summary>
    /// <remarks>The returned context is set up to include detailed error information and logs database
    /// commands at the information level. The database is asynchronously seeded with initial data upon creation. This
    /// method is intended for internal use when a properly configured database context is required.</remarks>
    /// <returns>A new <see cref="AppDbContext"/> instance configured with the current connection string and logging settings.</returns>
    private AppDbContext GetDbContext()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql($"{ConnectionString};Include Error Detail=true")
            .UseAsyncSeeding((context, _, _) => Seeder.SeedDatabaseAsync(context))
            .LogTo(
                message => Debug.WriteLine(message),
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .Options;

        return new AppDbContext(contextOptions);
    }
}
