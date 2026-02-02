namespace MotoLube.Domain.Entities;

public class Supplier : BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber {  get; set; }
    public string? Address {  get; set; }
    public bool IsActive { get; set; } = true;
}
