namespace GoodHamburger.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderResponse
{
    public long Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public DateTime UpdatedAt { get; set; }
}