﻿using Microsoft.Extensions.Options;
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

            _channel.BasicPublish(string.Empty, RabbitMqConstants.QUEUE_NAME, body: Encoding.UTF8.GetBytes(name));
        }
    }
}
