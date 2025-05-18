using Dapr.Messaging.PublishSubscribe;
using System.Text;
using System.Text.Json;
namespace DaprWrapper;
public class DaprSubscriptionBuilder<TMessage> : IAsyncDisposable
{
    private string _pubSubName = "pubsub";
    private string _topic = string.Empty;
    private TimeSpan _subscriptionTimeout = TimeSpan.FromSeconds(30);
    private TimeSpan _messageHandlingTimeout = TimeSpan.FromSeconds(10);
    private ISubscriber<TMessage>? _subscriber;
    private IAsyncDisposable? _subscription;

    public DaprSubscriptionBuilder<TMessage> WithPubSubName(string pubSubName) { _pubSubName = pubSubName; return this; }
    public DaprSubscriptionBuilder<TMessage> WithTopic(string topic) { _topic = topic; return this; }
    public DaprSubscriptionBuilder<TMessage> WithSubscriptionTimeout(TimeSpan timeout) { _subscriptionTimeout = timeout; return this; }
    public DaprSubscriptionBuilder<TMessage> WithMessageHandlingTimeout(TimeSpan timeout) { _messageHandlingTimeout = timeout; return this; }
    public DaprSubscriptionBuilder<TMessage> WithSubscriber(ISubscriber<TMessage> subscriber) { _subscriber = subscriber; return this; }

    public async Task StartAsync(DaprPublishSubscribeClient client)
    {
        if (_subscriber == null) throw new InvalidOperationException("Subscriber not provided.");
        if (string.IsNullOrWhiteSpace(_topic)) throw new InvalidOperationException("Topic is not specified.");

        var options = new DaprSubscriptionOptions(
            new MessageHandlingPolicy(_messageHandlingTimeout, TopicResponseAction.Retry));

       // var cancellationTokenSource = new CancellationTokenSource(_subscriptionTimeout);

        _subscription = await client.SubscribeAsync(
            _pubSubName,
            _topic,
            options,
            HandleTopicMessageAsync);
    }

    private async Task<TopicResponseAction> HandleTopicMessageAsync(TopicMessage message, CancellationToken cancellationToken)
    {
        try
        {
            var strData = Encoding.UTF8.GetString(message.Data.Span);
            var data = JsonSerializer.Deserialize<TMessage> (strData);
            if (data == null) return TopicResponseAction.Retry;

            return await _subscriber!.HandleMessageAsync(data, cancellationToken);
        }
        catch
        {
            return TopicResponseAction.Retry;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_subscription != null)
        {
            await _subscription.DisposeAsync();
        }
    }
}
