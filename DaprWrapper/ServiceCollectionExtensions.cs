using Dapr.Client;
using Dapr.Messaging.PublishSubscribe.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DaprWrapper;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSubscriber<TMessage, TSubscriber>(
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

    public static IServiceCollection AddPublisher<TMessage>(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDaprClient();

        services.AddSingleton<IPublisher<TMessage>>(sp =>
        {
            var daprClient = sp.GetRequiredService<DaprClient>();

            return new DaprPublisherBuilder<TMessage>()
                .ConfigureFrom(configuration)
                .WithClient(daprClient)
                .Build();
        });

        return services;
    }
}
