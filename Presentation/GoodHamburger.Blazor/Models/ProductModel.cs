namespace GoodHamburger.Blazor.Models;

public class ProductModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
}