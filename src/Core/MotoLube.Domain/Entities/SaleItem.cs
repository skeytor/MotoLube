namespace MotoLube.Domain.Entities;

public class SaleItem
{
    internal SaleItem() { }
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
