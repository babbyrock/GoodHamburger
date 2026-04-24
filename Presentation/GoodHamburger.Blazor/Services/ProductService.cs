using GoodHamburger.Blazor.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoodHamburger.Blazor.Services;

public class ProductService(HttpClient http)
{
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<ProductModel>> GetAllAsync() =>
        await http.GetFromJsonAsync<List<ProductModel>>("api/products", _options) ?? [];
}