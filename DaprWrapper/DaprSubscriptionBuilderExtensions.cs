using Microsoft.Extensions.Configuration;
namespace DaprWrapper;
public static class DaprSubscriptionBuilderExtensions
{
    public static DaprSubscriptionBuilder<TMessage> ConfigureFrom<TMessage>(
       this DaprSubscriptionBuilder<TMessage> builder,
       IConfiguration config)
    {
        var pubSubName = config["DaprSubscription:PubSubName"] ?? "pubsub";
        var topic = config["DaprSubscription:Topic"] ?? throw new InvalidOperationException("Missing Dapr:Topic setting");
        var subscriptionTimeout = TimeSpan.TryParse(config["DaprSubscription:SubscriptionTimeout"], out var st) ? st : TimeSpan.FromSeconds(30);
        var messageTimeout = TimeSpan.TryParse(config["DaprSubscription:MessageTimeout"], out var mt) ? mt : TimeSpan.FromSeconds(10);

        return builder
            .WithPubSubName(pubSubName)
            .WithTopic(topic)
            .WithSubscriptionTimeout(subscriptionTimeout)
            .WithMessageHandlingTimeout(messageTimeout);
    }
}