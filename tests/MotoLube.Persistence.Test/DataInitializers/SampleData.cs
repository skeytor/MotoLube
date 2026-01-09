using MotoLube.Domain.Entities;

namespace MotoLube.Persistence.Test.DataInitializers;

internal static class SampleData
{
    internal static readonly Category[] Categories =
    [
        new Category { Id = 1, Name = "Engine Oils" },
        new Category { Id = 2, Name = "Brake Fluids" },
        new Category { Id = 3, Name = "Coolants" },
    ];

    internal static readonly Brand[] Brands =
    [
        new Brand { Id = 1, Name = "MotoLube" },
        new Brand { Id = 2, Name = "SpeedyOil" },
        new Brand { Id = 3, Name = "BrakeMaster" },
    ];
    
    internal static readonly Supplier[] Suppliers =
    [
        new Supplier { Name = "Global Oils Co." },
        new Supplier { Name = "Brake Supplies Ltd." },
        new Supplier { Name = "Coolant Experts Inc." },
    ];

    internal static readonly Product[] Products =
    [
        new Product 
        { 
            Name = "MotoLube Synthetic 5W-30", 
            Price = 29.99m,
            Sku = "ML-SYN-5W30",
            Description = "High-performance synthetic engine oil for optimal protection and efficiency.",
            Tax = 0.20m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1, 
            BrandId = 1,
        },
        new Product 
        { 
            Name = "SpeedyOil Mineral 10W-40",
            Price = 19.99m,
            Sku = "SO-MIN-10W40",
            Description = "Reliable mineral engine oil for everyday riding conditions.",
            Tax = 0.18m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1, 
            BrandId = 2, 
        },
        new Product 
        { 
            Name = "BrakeMaster DOT 4", 
            Price = 14.99m,
            Sku = "BM-DOT4",
            Description = "High-performance brake fluid for superior stopping power.",
            Tax = 0.15m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 2, 
            BrandId = 3,
        },
        new Product 
        { 
            Name = "Coolant Experts Long-Life Coolant", 
            Price = 24.99m,
            Sku = "CE-LLC",
            Description = "Long-life coolant for optimal engine temperature regulation.",
            Tax = 0.12m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 3, 
            BrandId = 1,
        },
        new Product 
        { 
            Name = "MotoLube High Mileage 15W-50", 
            Price = 34.99m,
            Sku = "ML-HM-15W50",
            Description = "Engine oil designed for high-mileage motorcycles to reduce wear and tear.",
            Tax = 0.22m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1, 
            BrandId = 1,
        },
        new Product 
        { 
            Name = "SpeedyOil Synthetic Blend 10W-30", 
            Price = 22.99m,
            Sku = "SO-SB-10W30",
            Description = "Synthetic blend engine oil for enhanced performance and protection.",
            Tax = 0.19m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1, 
            BrandId = 2,
        },
        new Product 
        { 
            Name = "BrakeMaster DOT 5.1", 
            Price = 16.99m,
            Sku = "BM-DOT5.1",
            Description = "Advanced brake fluid for high-performance braking systems.",
            Tax = 0.16m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 2, 
            BrandId = 3,
        },
    ];
}
