using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Tests.Domain;

public class OrderItemValidationTests
{
    private static Product Sandwich(int id = 1) => new() { Id = id, Name = $"Sandwich {id}", Price = 5.00m, Type = ProductType.Sandwich };
    private static Product Fries() => new() { Id = 10, Name = "Batata Frita", Price = 2.00m, Type = ProductType.Fries };
    private static Product Drink() => new() { Id = 11, Name = "Refrigerante", Price = 2.50m, Type = ProductType.Drink };
    private static OrderItem ItemOf(Product p) => new() { Product = p, ProductId = p.Id };

    [Fact]
    public void Adding_duplicate_sandwich_returns_failure()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich(1)));
        var result = order.AddItem(ItemOf(Sandwich(2)));
        result.Success.Should().BeFalse();
        result.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Adding_duplicate_type_does_not_add_item()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich(1)));
        order.AddItem(ItemOf(Sandwich(2)));
        order.OrderItems.Should().HaveCount(1);
    }

    [Fact]
    public void Adding_different_types_succeeds()
    {
        var order = new Order();
        var r1 = order.AddItem(ItemOf(Sandwich()));
        var r2 = order.AddItem(ItemOf(Fries()));
        var r3 = order.AddItem(ItemOf(Drink()));
        r1.Success.Should().BeTrue();
        r2.Success.Should().BeTrue();
        r3.Success.Should().BeTrue();
        order.OrderItems.Should().HaveCount(3);
    }

    [Fact]
    public void ClearItems_removes_all_items()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.ClearItems();
        order.OrderItems.Should().BeEmpty();
    }

    [Fact]
    public void ClearItems_resets_discount_to_zero()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.ClearItems();
        order.DiscountPercentage.Should().Be(0.00m);
    }

    [Fact]
    public void ClearItems_resets_subtotal_to_zero()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.ClearItems();
        order.Subtotal.Should().Be(0);
    }
}