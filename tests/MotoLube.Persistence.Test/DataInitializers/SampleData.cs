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
        new Supplier { Id = Guid.NewGuid(), Name = "Global Oils Co." },
        new Supplier { Id = Guid.NewGuid(), Name = "Brake Supplies Ltd." },
        new Supplier { Id = Guid.NewGuid(), Name = "Coolant Experts Inc." },
    ];
}
