using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using VotingSystemBigBrotherBrasil.Common;
using VotingSystemBigBrotherBrasil.Consumer.App.Models.Settings;

namespace VotingSystemBigBrotherBrasil.Consumer.App.Services
{
    public class RabbitMqConsumerService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumerService(RabbitMqSettings rabbitMqSettings)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMqSettings.HostName,
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(RabbitMqConstants.QUEUE_NAME, RabbitMqConstants.QUEUE_DURABLE, RabbitMqConstants.QUEUE_EXCLUSIVE, RabbitMqConstants.QUEUE_AUTO_DELETE);
            }
            catch (Exception ex)
            {
                throw new Exception("RabbitMq Consumer error while instantiate", ex);
            }
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received {message}");
            };

            _channel.BasicConsume(
                queue: RabbitMqConstants.QUEUE_NAME,
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
