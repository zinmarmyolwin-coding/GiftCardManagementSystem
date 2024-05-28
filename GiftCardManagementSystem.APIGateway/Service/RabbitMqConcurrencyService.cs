using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.APIGateway.Service
{
    public class RabbitMqConcurrencyService : IDisposable
    {
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly SemaphoreSlim _semaphore;

        public RabbitMqConcurrencyService(IModel channel, int maxConcurrentRequests)
        {
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _semaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += Consumer_Received;
        }

        public void StartConsuming()
        {
            _channel.QueueDeclare(queue: "myqueue",
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
            _channel.BasicConsume(queue: "myqueue", autoAck: false, consumer: _consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            _semaphore.Wait();

            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                 routingKey: "myqueue",
                                 basicProperties: null,
                                 body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
