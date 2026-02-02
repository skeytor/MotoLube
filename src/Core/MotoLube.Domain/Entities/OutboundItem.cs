namespace MotoLube.Domain.Entities;

public class OutboundItem
{
    public Guid OutboundId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
