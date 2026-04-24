namespace GoodHamburger.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdResponse
{
    public long Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}