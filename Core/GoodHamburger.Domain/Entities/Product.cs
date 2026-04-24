using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
}