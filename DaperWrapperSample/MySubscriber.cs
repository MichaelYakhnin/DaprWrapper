using System.Text;
using Dapr.Messaging.PublishSubscribe;
using DaprWrapper;
using DaprWrapperSample.Models;


namespace DaprWrapperSample;
public class MySubscriber : ISubscriber<Order>
{
    public Task<TopicResponseAction> HandleMessageAsync(Order message, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Received: {message}");
        return Task.FromResult(TopicResponseAction.Success);
    }
}