namespace MotoLube.Domain.Entities;

public class Brand : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = [];
}