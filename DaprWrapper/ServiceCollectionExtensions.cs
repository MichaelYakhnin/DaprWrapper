using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dapr.Messaging.PublishSubscribe.Extensions;
namespace DaprWrapper;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDaprStreamingSubscriber<TMessage, TSubscriber>(
        this IServiceCollection services, IConfiguration config)
        where TSubscriber : class, ISubscriber<TMessage>
    {
        services.AddDaprPubSubClient();
        services.AddSingleton<ISubscriber<TMessage>, TSubscriber>();

        services.AddSingleton(provider =>
        {
            var builder = new DaprSubscriptionBuilder<TMessage>();
            builder.WithSubscriber(provider.GetRequiredService<ISubscriber<TMessage>>())
                   .ConfigureFrom(config);
            return builder;
        });

        return services;
    }
}
