using GoodHamburger.Application;
using GoodHamburger.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Blazor", policy =>
    policy.WithOrigins("https://localhost:7254")
          .AllowAnyHeader()
          .AllowAnyMethod());
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Blazor");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }