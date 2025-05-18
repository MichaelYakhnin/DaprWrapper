using Dapr.Messaging.PublishSubscribe;
namespace DaprWrapper;

public interface ISubscriber<TMessage>
{
    Task<TopicResponseAction> HandleMessageAsync(TMessage message, CancellationToken cancellationToken = default);
}
