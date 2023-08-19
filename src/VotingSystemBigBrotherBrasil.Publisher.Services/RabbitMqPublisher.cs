using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using VotingSystemBigBrotherBrasil.Publisher.Models.Settings;

namespace VotingSystemBigBrotherBrasil.Publisher.Services
{
    public class RabbitMqPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
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
            var queueName = "bbb-votes-queue";

            _channel.QueueDeclare(queueName, true, false, false);

            _channel.BasicPublish(string.Empty, queueName, body: Encoding.UTF8.GetBytes(name));
        }
    }
}
