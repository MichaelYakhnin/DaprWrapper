using Dapr.Client;
using DaprWrapper;
using DaprWrapperPublisher.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPublisher<Order>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/weatherforecast", async (DaprClient client) =>
{
    var publisher = app.Services.GetRequiredService<IPublisher<Order>>();
    await publisher.PublishAsync(new Order("123", "customer-456", 99, DateTime.UtcNow));
    return Results.Ok();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();


