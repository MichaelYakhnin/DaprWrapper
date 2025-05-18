namespace DaprWrapper;
public interface IPublisher<TMessage>
{
    Task PublishAsync(TMessage message, CancellationToken cancellationToken = default);
}
