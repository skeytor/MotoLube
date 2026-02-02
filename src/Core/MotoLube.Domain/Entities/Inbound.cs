namespace MotoLube.Domain.Entities;

public class Inbound : BaseEntity<Guid>
{
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public DateTimeOffset InboundDate { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public ICollection<InboundItem> Items { get; init; } = [];
    public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
}