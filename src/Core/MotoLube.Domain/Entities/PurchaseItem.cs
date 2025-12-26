namespace MotoLube.Domain.Entities;

public class PurchaseItem
{
    internal PurchaseItem() { }

    public Guid PurchaseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
