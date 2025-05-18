using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaprWrapper;
public static class DaprPublisherBuilderExtensions
{
    public static DaprPublisherBuilder<TMessage> ConfigureFrom<TMessage>(
            this DaprPublisherBuilder<TMessage> builder,
            IConfiguration configuration)
    {
        var pubSubName = configuration["DaprPublisher:PubSubName"] ?? "pubsub";
        var topic = configuration["DaprPublisher:Topic"] ?? throw new InvalidOperationException("Missing Dapr:Topic setting");

        return builder
            .WithPubSubName(pubSubName)
            .WithTopic(topic);
    }
}
