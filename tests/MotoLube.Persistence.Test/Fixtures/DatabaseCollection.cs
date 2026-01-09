namespace MotoLube.Persistence.Test.Fixtures;

/// <summary>
/// Defines a test collection for integration tests that share access to a common database fixture.
/// </summary>
/// <remarks>Use this collection to group tests that require shared setup or teardown of database resources. Tests
/// within this collection will use the same instance of <see cref="DatabaseFixture"/>, ensuring consistent state across
/// related tests. This is typically used to avoid repeated initialization and to coordinate resource usage in test
/// scenarios.</remarks>
[CollectionDefinition(nameof(DatabaseCollection))]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>;
