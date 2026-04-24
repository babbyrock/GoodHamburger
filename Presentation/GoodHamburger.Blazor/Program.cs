using GoodHamburger.Blazor;
using GoodHamburger.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7174")
});

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();