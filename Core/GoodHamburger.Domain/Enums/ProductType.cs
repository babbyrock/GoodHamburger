using System.Text.Json.Serialization;

namespace GoodHamburger.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductType
{
    Sandwich = 1,
    Fries = 2,
    Drink = 3
}