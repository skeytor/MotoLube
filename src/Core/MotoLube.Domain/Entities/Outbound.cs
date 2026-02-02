namespace MotoLube.Domain.Entities;

public class Outbound : BaseEntity<Guid>
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTimeOffset OutboundDate { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public ICollection<OutboundItem> Items { get; init; } = [];
    public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
}
