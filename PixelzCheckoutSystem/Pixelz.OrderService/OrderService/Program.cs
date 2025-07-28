using OrderService.Data;
using OrderService.Services;
using Microsoft.EntityFrameworkCore;
using Pixelz.Models.Common;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseInMemoryDatabase("OrderDb"));
builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("Services"));

builder.Services.AddHttpClient("Payment", (sp, client) =>
{
    var config = sp.GetRequiredService<IOptions<ServiceUrls>>().Value;
    client.BaseAddress = new Uri(config.Payment.BaseUrl);
});

builder.Services.AddHttpClient("Production", (sp, client) =>
{
    var config = sp.GetRequiredService<IOptions<ServiceUrls>>().Value;
    client.BaseAddress = new Uri(config.Production.BaseUrl);
});

builder.Services.AddHttpClient("Email", (sp, client) =>
{
    var config = sp.GetRequiredService<IOptions<ServiceUrls>>().Value;
    client.BaseAddress = new Uri(config.Email.BaseUrl);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SampleDataSeeder.Seed(app);

app.Run();
