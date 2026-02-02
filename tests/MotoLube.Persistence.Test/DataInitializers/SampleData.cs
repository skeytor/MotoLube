using MotoLube.Domain.Entities;

namespace MotoLube.Persistence.Test.DataInitializers;

internal static class SampleData
{
    internal static readonly Category[] Categories =
    [
        new() { Id = 1, Name = "Engine Oils" },
        new() { Id = 2, Name = "Brake Fluids" },
        new() { Id = 3, Name = "Coolants" },
    ];

    internal static readonly Brand[] Brands =
    [
        new() { Id = 1, Name = "MotoLube" },
        new() { Id = 2, Name = "SpeedyOil" },
        new() { Id = 3, Name = "BrakeMaster" },
    ];

    internal static readonly Supplier[] Suppliers =
    [
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Global Oils Co." },
        new() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Brake Supplies Ltd." },
        new() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Coolant Experts Inc." },
    ];

    internal static readonly Product[] Products =
    [
        new()
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "MotoLube Synthetic 5W-30",
            Price = 29.99m,
            Sku = "ML-SYN-5W30",
            Description = "High-performance synthetic engine oil for optimal protection and efficiency.",
            Tax = 0.20m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1,
            BrandId = 1,
        },
        new()
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Name = "SpeedyOil Mineral 10W-40",
            Price = 19.99m,
            Sku = "SO-MIN-10W40",
            Description = "Reliable mineral engine oil for everyday riding conditions.",
            Tax = 0.18m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1,
            BrandId = 2,
        },
        new()
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            Name = "BrakeMaster DOT 4",
            Price = 14.99m,
            Sku = "BM-DOT4",
            Description = "High-performance brake fluid for superior stopping power.",
            Tax = 0.15m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 2,
            BrandId = 3,
        },
        new()
        {
            Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            Name = "Coolant Experts Long-Life Coolant",
            Price = 24.99m,
            Sku = "CE-LLC",
            Description = "Long-life coolant for optimal engine temperature regulation.",
            Tax = 0.12m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 3,
            BrandId = 1,
        },
        new()
        {
            Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            Name = "MotoLube High Mileage 15W-50",
            Price = 34.99m,
            Sku = "ML-HM-15W50",
            Description = "Engine oil designed for high-mileage motorcycles to reduce wear and tear.",
            Tax = 0.22m,
            CreatedAt = DateTimeOffset.UtcNow,
            CategoryId = 1,
            BrandId = 1,
        },
        new()
        {
            Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            Name = "SpeedyOil Synthetic Blend 10W-30",
            Price = 22.99m,
            Sku = "SO-SB-10W30",
            Description = "Synthetic blend engine oil for enhanced performance and protection.",
            Tax = 0.19m,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-7),
            CategoryId = 1,
            BrandId = 2,
        },
        new()
        {
            Id = Guid.Parse("12121212-1212-1212-1212-121212121212"),
            Name = "BrakeMaster DOT 5.1",
            Price = 16.99m,
            Sku = "BM-DOT5.1",
            Description = "Advanced brake fluid for high-performance braking systems.",
            Tax = 0.16m,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-30),
            CategoryId = 2,
            BrandId = 3,
        },
    ];

    internal static readonly Inbound[] Inbounds =
    [
        new Inbound
        {
            Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
            InboundDate = DateTimeOffset.UtcNow.AddDays(-1),
            SupplierId = Suppliers[0].Id,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-1),
            Items =
            [
                new()
                {
                    ProductId = Products[0].Id,
                    Quantity = 100,
                    UnitPrice = 25.00m
                },
                new()
                {
                    ProductId = Products[1].Id,
                    Quantity = 150,
                    UnitPrice = 18.00m
                }
            ]
        },
        new Inbound
        {
            Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
            InboundDate = DateTimeOffset.UtcNow.AddDays(-5),
            SupplierId = Suppliers[1].Id,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            Items =
            [
                new()
                {
                    ProductId = Products[2].Id,
                    Quantity = 200,
                    UnitPrice = 12.00m
                },
                new() 
                {
                    ProductId = Products[3].Id,
                    Quantity = 120,
                    UnitPrice = 20.00m
                }
            ],
        },
        new Inbound 
        {
            Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
            InboundDate = DateTimeOffset.UtcNow.AddDays(-40),
            SupplierId = Suppliers[2].Id,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-40),
            Items =
            [
                new()
                {
                    ProductId = Products[4].Id,
                    Quantity = 100,
                    UnitPrice = 30.00m
                },
                new()
                {
                    ProductId = Products[5].Id,
                    Quantity = 150,
                    UnitPrice = 25.00m
                }
            ]
        }
    ];
}
