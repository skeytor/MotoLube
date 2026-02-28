namespace MotoLube.Application.DTOs.Requests;

public sealed record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Sku,
    int CategoryId,
    int BrandId);