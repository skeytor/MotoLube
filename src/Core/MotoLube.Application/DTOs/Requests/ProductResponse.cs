namespace MotoLube.Application.DTOs.Requests;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Sku,
    int StockQuantity
);