using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoLube.Persistence.Test.DataInitializers;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace MotoLube.Persistence.Test.Fixtures;

public sealed class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer? _container;

    public AppDbContext Context { get; private set; } = null!;
    public string ConnectionString => 
        _container?.GetConnectionString() ?? 
        throw new InvalidOperationException("Container not initialized.");

    public Task DisposeAsync() => _container!.DisposeAsync().AsTask();

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder("postgres:latest").Build();
        await _container.StartAsync();
        Context = GetDbContext();
        await Context.Database.EnsureCreatedAsync();
    }

    private AppDbContext GetDbContext()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql($"{ConnectionString};Include Error Detail=true")
            .UseAsyncSeeding((context, _, _) => DataInitializer.SeedDatabaseAsync(context))
            .LogTo(
                message => Debug.WriteLine(message),
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .Options;

        return new AppDbContext(contextOptions);
    }
}
