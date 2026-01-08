namespace MotoLube.Persistence.Test.DataInitializers;

internal static class DataInitializer
{
    internal static async Task SeedDatabaseAsync(AppDbContext context)
    {
        context.Categories.AddRange(SampleData.Categories);
        context.Brands.AddRange(SampleData.Brands);
        context.Suppliers.AddRange(SampleData.Suppliers);
        context.Products.AddRange(SampleData.Products);
        await context.SaveChangesAsync();
    }
}
