# Order Created Event
dapr publish --publish-app-id order-processor --pubsub pubsub --topic orders --data '{
    "orderId": "order-123",
    "customerId": "customer-456",
    "amount": 99.99,
    "createdAt": "2025-05-16T14:30:00Z"
}'

# Wait a bit
Start-Sleep -Seconds 5

# Order Paid Event
dapr publish --publish-app-id order-processor --pubsub pubsub --topic orders --data '{
    "orderId": "order-123",
    "paymentId": "payment-789",
    "amount": 99.99,
    "createdAt": "2025-05-16T14:31:00Z"
}'

# Wait a bit
Start-Sleep -Seconds 5

# Order Shipped Event
dapr publish --publish-app-id order-processor --pubsub pubsub --topic orders --data '{
    "orderId": "order-123",
    "trackingNumber": "1Z999AA1234567890",
    "createdAt": "2025-05-16T14:32:00Z"
}'