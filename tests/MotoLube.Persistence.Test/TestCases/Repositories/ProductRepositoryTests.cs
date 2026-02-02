using MotoLube.Domain.Entities;
using MotoLube.Persistence.Repositories;
using MotoLube.Persistence.Test.DataInitializers;
using MotoLube.Persistence.Test.Fixtures;
using SharedKernel.Pagination;
using Xunit.Abstractions;

namespace MotoLube.Persistence.Test.TestCases.Repositories;

public class ProductRepositoryTests(DatabaseFixture database, ITestOutputHelper outputHelper) 
    : TestBase(database, outputHelper)
{
    private readonly ProductRepository _repository = new(database.Context);

    #region FindByIdAsync Tests

    [Fact]
    public async Task FindByIdAsync_Should_Return_Product_When_ProductExists()
    {
        // Arrange
        Product existingProduct = SampleData.Products[0];

        // Act
        var result = await _repository.FindByIdAsync(existingProduct.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingProduct.Id, result.Id);
        Assert.Equal(existingProduct.Name, result.Name);
        Assert.Equal(existingProduct.Sku, result.Sku);
    }

    [Fact]
    public async Task FindByIdAsync_Should_Return_Null_When_ProductDoesNotExist()
    {
        // Arrange
        Guid nonExistentId = Guid.NewGuid();

        // Act
        var result = await _repository.FindByIdAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region FindBySkuAsync Tests

    [Fact]
    public async Task FindBySkuAsync_Should_Return_Product_When_SkuExists()
    {
        // Arrange
        Product existingProduct = SampleData.Products[0];

        // Act
        var result = await _repository.FindBySkuAsync(existingProduct.Sku);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingProduct.Sku, result.Sku);
        Assert.Equal(existingProduct.Name, result.Name);
    }

    [Theory]
    [InlineData("NON-EXISTENT-SKU")]
    [InlineData("123-INVALID-SKU")]
    public async Task FindBySkuAsync_Should_Return_Null_When_SkuDoesNotExist(string nonExistentSku)
    {
        // Arrange

        // Act
        var result = await _repository.FindBySkuAsync(nonExistentSku);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region IsSkuUniqueAsync Tests

    [Fact]
    public async Task IsSkuUniqueAsync_Should_Return_False_When_SkuAlreadyExists()
    {
        // Arrange
        string existingSku = SampleData.Products[0].Sku;

        // Act
        var result = await _repository.IsSkuUniqueAsync(existingSku);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("NEW-SKU-001")]
    [InlineData("ANOTHER-UNIQUE-SKU")]
    [InlineData("UNIQUE-SKU-12345")]
    public async Task IsSkuUniqueAsync_Should_Return_True_When_SkuDoesNotExist(string uniqueSku)
    {
        // Act
        var result = await _repository.IsSkuUniqueAsync(uniqueSku);

        // Assert
        Assert.True(result);
    }

    #endregion

    #region CountAsync Tests

    [Fact]
    public async Task CountAsync_Should_Return_TotalNumberOfProducts()
    {
        // Arrange
        int expectedCount = SampleData.Products.Length;

        // Act
        var result = await _repository.CountAsync();

        // Assert
        Assert.Equal(expectedCount, result);
    }

    #endregion

    #region GetPagedAsync Tests

    [Theory]
    [InlineData(1, 3, 3)]
    [InlineData(2, 3, 3)]
    [InlineData(3, 3, 1)]
    public async Task GetPagedAsync_Should_Return_PagedProducts(int page, int size, int expectedCount)
    {
        // Arrange
        PaginationOptions options = new(Page: page, Size: size);

        // Act
        var result = await _repository.GetPagedAsync(options, p => p);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public async Task GetPagedAsync_Should_Return_RemainingProducts_When_LastPageRequested()
    {
        // Arrange
        int totalProducts = SampleData.Products.Length;
        int pageSize = 3;
        int lastPage = (int)Math.Ceiling(totalProducts / (double)pageSize);
        int expectedCount = totalProducts - (pageSize * (lastPage - 1));
        PaginationOptions options = new(lastPage, pageSize);

        // Act
        var result = await _repository.GetPagedAsync(options, p => p);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(100, 10)]
    [InlineData(2, 0)]
    public async Task GetPagedAsync_Should_Return_EmptyList_When_PageExceedsTotalPages(int page, int size)
    {
        // Arrange
        PaginationOptions options = new(page, size);

        // Act
        var result = await _repository.GetPagedAsync(options, p => p);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(1, 3)]
    public async Task GetPagedAsync_WithSelector_Should_Return_ProjectedResults(int page, int size)
    {
        // Arrange
        PaginationOptions filter = new(page, size);

        // Act
        var result = await _repository.GetPagedAsync(filter, p => new { p.Id, p.Name });

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.False(string.IsNullOrEmpty(item.Name));
        });
    }

    #endregion

    #region InsertAsync Tests

    [Fact]
    public Task InsertAsync_Should_Return_InsertedProduct_When_ValidProductProvided()
    {
        return ExecuteInTransactionAsync(async () =>
        {
            // Arrange
            Product newProduct = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Price = 99.99m,
                Sku = "TEST-SKU-001",
                Description = "Test product description",
                Tax = 0.10m,
                CreatedAt = DateTimeOffset.UtcNow,
                CategoryId = 1,
                BrandId = 1
            };

            // Act
            var result = await _repository.InsertAsync(newProduct);
            await Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newProduct.Id, result.Id);
            Assert.Equal(newProduct.Name, result.Name);

            Product? insertedProduct = await _repository.FindByIdAsync(newProduct.Id);
            Assert.NotNull(insertedProduct);
        });
    }

    #endregion

    #region Update Tests

    [Fact]
    public Task Update_Should_ModifyProduct_When_ValidChangesProvided()
    {
        return ExecuteInTransactionAsync(async () =>
        {
            // Arrange
            Product? existingProduct = await _repository.FindByIdAsync(SampleData.Products[0].Id);
            Assert.NotNull(existingProduct);

            string updatedName = "Updated Product Name";
            decimal updatedPrice = 199.99m;
            existingProduct.Name = updatedName;
            existingProduct.Price = updatedPrice;

            // Act
            _repository.Update(existingProduct);
            await Context.SaveChangesAsync();

            // Assert
            Product? modifiedProduct = await _repository.FindByIdAsync(existingProduct.Id);
            Assert.NotNull(modifiedProduct);
            Assert.Equal(updatedName, modifiedProduct.Name);
            Assert.Equal(updatedPrice, modifiedProduct.Price);
        });
    }

    #endregion
}