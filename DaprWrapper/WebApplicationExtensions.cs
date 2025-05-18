using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Dapr.Messaging.PublishSubscribe;
namespace DaprWrapper;
public static class WebApplicationExtensions
{
    public static WebApplication UseDaprStreamingSubscription<TMessage>(this WebApplication app)
    {
        var builder = app.Services.GetRequiredService<DaprSubscriptionBuilder<TMessage>>();
        var client = app.Services.GetRequiredService<DaprPublishSubscribeClient>();

        _ = builder.StartAsync(client);

        app.Lifetime.ApplicationStopping.Register(async () => await builder.DisposeAsync());

        return app;
    }
}