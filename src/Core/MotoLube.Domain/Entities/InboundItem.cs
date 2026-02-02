namespace MotoLube.Domain.Entities;

public class InboundItem
{
    public Guid InboundId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}