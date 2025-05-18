using System;
using System.Text.Json.Serialization;

namespace DaprWrapperPublisher.Models
{
    public record Order(
        [property: JsonPropertyName("orderId")] string OrderId,
        [property: JsonPropertyName("customerId")] string CustomerId,
        [property: JsonPropertyName("amount")] decimal Amount,
        [property: JsonPropertyName("createdAt")] DateTime CreatedAt);

    //public record OrderPaid(
    //    string OrderId,
    //    string PaymentId,
    //    decimal Amount,
    //    DateTime PaidAt);

    //public record OrderShipped(
    //    string OrderId,
    //    string TrackingNumber,
    //    DateTime ShippedAt);
}