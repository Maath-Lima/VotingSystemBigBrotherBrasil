using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using VotingSystemBigBrotherBrasil.Common;
using VotingSystemBigBrotherBrasil.Publisher.Models.Settings;

namespace VotingSystemBigBrotherBrasil.Publisher.Services
{
    public class RabbitMqPublisherService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisherService(IOptions<RabbitMqSettings> options)
        {
            var rabbitMqSettings = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqSettings.HostName,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishVote(string name)
        {
            _channel.QueueDeclare(RabbitMqConstants.QUEUE_NAME, RabbitMqConstants.QUEUE_DURABLE, RabbitMqConstants.QUEUE_EXCLUSIVE, RabbitMqConstants.QUEUE_AUTO_DELETE);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            _channel.BasicPublish(ConfigQueueExchange(), RabbitMqConstants.QUEUE_NAME, ConfigQueueProperties(), Encoding.UTF8.GetBytes(name));
        }

        private IBasicProperties ConfigQueueProperties()
        {
            var properties = _channel.CreateBasicProperties();

            properties.Persistent = true;

            return properties;
        }

        private string ConfigQueueExchange()
        {
            _channel.ExchangeDeclare(RabbitMqConstants.EXCHANGE_NAME, ExchangeType.Direct);

            return RabbitMqConstants.EXCHANGE_NAME;
        }
    }
}
