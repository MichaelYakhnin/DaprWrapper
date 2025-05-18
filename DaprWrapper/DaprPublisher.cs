
using Dapr.Client;

namespace DaprWrapper;
public class DaprPublisher<TMessage> : IPublisher<TMessage>
{
    private readonly DaprClient _client;
    private readonly string _pubSubName;
    private readonly string _topic;

    public DaprPublisher(DaprClient client, string pubSubName, string topic)
    {
        _client = client;
        _pubSubName = pubSubName;
        _topic = topic;
    }

    public Task PublishAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        return _client.PublishEventAsync(_pubSubName, _topic, message, cancellationToken);
    }
}
