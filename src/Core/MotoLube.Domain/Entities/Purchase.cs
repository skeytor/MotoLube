namespace MotoLube.Domain.Entities;

public class Purchase
{
    private readonly HashSet<PurchaseItem> _items = [];

    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public DateTimeOffset PurchaseDate { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public decimal TotalAmount => _items.Sum(i => i.UnitPrice * i.Quantity);
    public IReadOnlySet<PurchaseItem> Items => _items;
}