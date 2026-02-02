namespace MotoLube.Persistence.Test.DataInitializers;

/// <summary>
/// Provides methods for initializing an application database with predefined sample data.
/// </summary>
/// <remarks>This class is intended for internal use during development or testing to populate the database with
/// sample entities. All members are static and should be called as part of the application's setup or migration
/// process. This class is not thread-safe.</remarks>
internal static class Seeder
{
    /// <summary>
    /// Asynchronously seeds the specified database context with sample data.
    /// </summary>
    /// <remarks>This method adds predefined sample entities to the context and saves the changes. Intended
    /// for use in development or testing scenarios to initialize the database with example data.</remarks>
    /// <param name="context">The database context to populate with sample data. Must not be null.</param>
    /// <returns>A task that represents the asynchronous seeding operation.</returns>
    internal static async Task SeedDatabaseAsync(AppDbContext context)
    {
        context.Categories.AddRange(SampleData.Categories);
        context.Brands.AddRange(SampleData.Brands);
        context.Suppliers.AddRange(SampleData.Suppliers);
        context.Products.AddRange(SampleData.Products);
        context.Inbounds.AddRange(SampleData.Inbounds);
        await context.SaveChangesAsync();
    }
}