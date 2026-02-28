using MotoLube.Application.DTOs.Requests;
using MotoLube.Domain.Entities;

namespace MotoLube.Application.Extensions.Mappers;

internal static class ProductMapperExtensions
{
    extension(Product source)
    {
        public ProductResponse ToResponse() => 
            new(source.Id,
                source.Name,
                source.Description!,
                source.Price,
                source.Sku,
                source.Stock);
    }

    extension(CreateProductRequest source)
    {
        public Product ToEntity() => 
            new()
            {
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                Sku = source.Sku,
                Stock = source.StockQuantity,
                CategoryId = source.CategoryId,
                BrandId = source.BrandId,
            };
    }

    extension(UpdateProductRequest source)
    {
        public void ApplyTo(Product target)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Sku = source.Sku;
            target.CategoryId = source.CategoryId;
            target.BrandId = source.BrandId;
        }
    }
}