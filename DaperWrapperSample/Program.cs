using DaprWrapper;
using DaprWrapperSample;
using DaprWrapperSample.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSubscriber<Order,MySubscriber>(builder.Configuration);

var app = builder.Build();
app.UseDaprStreamingSubscription<Order>();


app.Run();

