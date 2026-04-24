namespace GoodHamburger.Domain.Entities;

public class OrderItem : BaseEntity
{
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public long ProductId { get; set; }
    public Product Product { get; set; } = null!;
}