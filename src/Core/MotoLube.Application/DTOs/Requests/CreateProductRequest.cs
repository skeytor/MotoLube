using System.ComponentModel.DataAnnotations;

namespace MotoLube.Application.DTOs.Requests;

public sealed record CreateProductRequest(
    [Required, Length(1, 100)] string Name,
    [Required, Length(1, 150)] string Description,
    [Required] decimal Price,
    [Required, Length(1, 25)] string Sku,
    [Required] int StockQuantity,
    [Required] int CategoryId,
    [Required] int BrandId
);
