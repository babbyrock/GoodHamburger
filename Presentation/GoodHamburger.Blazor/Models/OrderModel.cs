namespace GoodHamburger.Blazor.Models;

public class OrderModel
{
    public long Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemModel> Items { get; set; } = [];
}

public class OrderItemModel
{
    public long ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
}