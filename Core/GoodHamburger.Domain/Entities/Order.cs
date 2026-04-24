using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public decimal Subtotal => _orderItems.Sum(i => i.Product?.Price ?? 0);
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount => Subtotal * DiscountPercentage;
    public decimal Total => Subtotal - DiscountAmount;

    public (bool Success, string? Error) AddItem(OrderItem item)
    {
        if (_orderItems.Any(i => i.Product?.Type == item.Product?.Type))
            return (false, $"Já existe um item do tipo '{item.Product?.Type}' neste pedido.");

        _orderItems.Add(item);
        RecalculateDiscount();
        return (true, null);
    }

    public void ClearItems()
    {
        _orderItems.Clear();
        RecalculateDiscount();
    }

    private void RecalculateDiscount()
    {
        bool hasSandwich = _orderItems.Any(i => i.Product?.Type == ProductType.Sandwich);
        bool hasFries = _orderItems.Any(i => i.Product?.Type == ProductType.Fries);
        bool hasDrink = _orderItems.Any(i => i.Product?.Type == ProductType.Drink);

        DiscountPercentage = (hasSandwich, hasFries, hasDrink) switch
        {
            (true, true, true) => 0.20m,
            (true, false, true) => 0.15m,
            (true, true, false) => 0.10m,
            _ => 0.00m
        };
    }
}