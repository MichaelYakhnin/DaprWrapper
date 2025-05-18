namespace DaprWrapper;
public class DaprSubscriptionConfig
{
    public string PubSubName { get; set; } = "pubsub";
    public string Topic { get; set; } = string.Empty;
    public int SubscriptionTimeoutSeconds { get; set; } = 30;
    public int MessageHandlingTimeoutSeconds { get; set; } = 10;
}