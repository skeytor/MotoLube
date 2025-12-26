namespace MotoLube.Domain.Entities;

public class Sale
{
    private readonly HashSet<SaleItem> _items = [];

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTimeOffset SaleDate { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public decimal TotalAmount => _items.Sum(i => i.UnitPrice * i.Quantity);
    public IReadOnlySet<SaleItem> Items => _items;
}
