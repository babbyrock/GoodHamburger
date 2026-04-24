using GoodHamburger.Blazor.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoodHamburger.Blazor.Services;

public class OrderService(HttpClient http)
{
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<OrderModel>> GetAllAsync() =>
        await http.GetFromJsonAsync<List<OrderModel>>("api/orders", _options) ?? [];

    public async Task<OrderModel?> GetByIdAsync(long id) =>
        await http.GetFromJsonAsync<OrderModel>($"api/orders/{id}", _options);

    public async Task<bool> CreateAsync(CreateOrderModel model)
    {
        var response = await http.PostAsJsonAsync("api/orders", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var response = await http.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
}