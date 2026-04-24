using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Tests.Domain;

public class OrderDiscountTests
{
    private static Product Sandwich() => new() { Id = 1, Name = "X Burger", Price = 5.00m, Type = ProductType.Sandwich };
    private static Product Fries() => new() { Id = 2, Name = "Batata Frita", Price = 2.00m, Type = ProductType.Fries };
    private static Product Drink() => new() { Id = 3, Name = "Refrigerante", Price = 2.50m, Type = ProductType.Drink };
    private static OrderItem ItemOf(Product p) => new() { Product = p, ProductId = p.Id };

    [Fact]
    public void Sandwich_Fries_Drink_gives_20_percent()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.AddItem(ItemOf(Drink()));
        order.DiscountPercentage.Should().Be(0.20m);
    }

    [Fact]
    public void Sandwich_Drink_gives_15_percent()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Drink()));
        order.DiscountPercentage.Should().Be(0.15m);
    }

    [Fact]
    public void Sandwich_Fries_gives_10_percent()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.DiscountPercentage.Should().Be(0.10m);
    }

    [Fact]
    public void No_sandwich_gives_no_discount()
    {
        var order = new Order();
        order.AddItem(ItemOf(Fries()));
        order.AddItem(ItemOf(Drink()));
        order.DiscountPercentage.Should().Be(0.00m);
    }

    [Fact]
    public void Only_sandwich_gives_no_discount()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.DiscountPercentage.Should().Be(0.00m);
    }

    [Fact]
    public void Subtotal_is_sum_of_all_items()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.AddItem(ItemOf(Drink()));
        order.Subtotal.Should().Be(9.50m);
    }

    [Fact]
    public void Total_with_20_percent_discount_is_correct()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.AddItem(ItemOf(Drink()));
        order.Total.Should().Be(7.60m);
    }

    [Fact]
    public void Total_with_15_percent_discount_is_correct()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Drink()));
        order.Total.Should().Be(6.375m);
    }

    [Fact]
    public void Total_with_10_percent_discount_is_correct()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.Total.Should().Be(6.30m);
    }

    [Fact]
    public void DiscountAmount_is_subtotal_times_percentage()
    {
        var order = new Order();
        order.AddItem(ItemOf(Sandwich()));
        order.AddItem(ItemOf(Fries()));
        order.AddItem(ItemOf(Drink()));
        order.DiscountAmount.Should().Be(order.Subtotal * 0.20m);
    }
}