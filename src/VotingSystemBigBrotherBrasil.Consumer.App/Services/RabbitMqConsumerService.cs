using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using VotingSystemBigBrotherBrasil.Common;
using VotingSystemBigBrotherBrasil.Consumer.App.Models.Settings;
using VotingSystemBigBrotherBrasil.Consumer.Data;

namespace VotingSystemBigBrotherBrasil.Consumer.App.Services
{
    public class RabbitMqConsumerService
    {
        private const int MILLISECONDS_VALUE = 1000;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly VotingSystemContext _context;

        public RabbitMqConsumerService(RabbitMqSettings rabbitMqSettings, VotingSystemContext context)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMqSettings.HostName,
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(RabbitMqConstants.QUEUE_NAME_PREFIX, RabbitMqConstants.QUEUE_DURABLE, RabbitMqConstants.QUEUE_EXCLUSIVE, RabbitMqConstants.QUEUE_AUTO_DELETE);

                _context = context;
            }
            catch (Exception ex)
            {
                throw new Exception("RabbitMq Consumer error while instantiate", ex);
            }
        }

        public void Start()
        {
            _channel.ExchangeDeclare(RabbitMqConstants.EXCHANGE_NAME, ExchangeType.Direct);

            _channel.QueueBind(RabbitMqConstants.QUEUE_NAME_PREFIX, RabbitMqConstants.EXCHANGE_NAME, string.Empty);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Received {message}");

                var elelapsedTime = message.Split(" ").Length;

                Thread.Sleep(elelapsedTime * MILLISECONDS_VALUE);

                var commit = _context.Commit(message);

                if (commit) Console.WriteLine($"Received {message} in database");

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(
                queue: RabbitMqConstants.QUEUE_NAME_PREFIX,
                autoAck: false,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
