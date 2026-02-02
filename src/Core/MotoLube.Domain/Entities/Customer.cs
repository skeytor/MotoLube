namespace MotoLube.Domain.Entities;

public sealed class Customer : BaseEntity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
