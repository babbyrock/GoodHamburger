using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Products.Queries.GetProducts;

public class GetProductsResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
}