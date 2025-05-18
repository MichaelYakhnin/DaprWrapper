
using Dapr.Client;

namespace DaprWrapper;
public class DaprPublisherBuilder<TMessage>
{
    private string? _pubSubName;
    private string? _topic;
    private DaprClient? _client;

    public DaprPublisherBuilder<TMessage> WithPubSubName(string pubSubName)
    {
        _pubSubName = pubSubName;
        return this;
    }

    public DaprPublisherBuilder<TMessage> WithTopic(string topic)
    {
        _topic = topic;
        return this;
    }

    public DaprPublisherBuilder<TMessage> WithClient(DaprClient client)
    {
        _client = client;
        return this;
    }


    public IPublisher<TMessage> Build()
    {
        if (_client == null)
            throw new InvalidOperationException("DaprClient must be provided.");
        if (string.IsNullOrWhiteSpace(_pubSubName))
            throw new InvalidOperationException("PubSub name must be provided.");
        if (string.IsNullOrWhiteSpace(_topic))
            throw new InvalidOperationException("Topic must be provided.");

        return new DaprPublisher<TMessage>(_client, _pubSubName, _topic);
    }
}