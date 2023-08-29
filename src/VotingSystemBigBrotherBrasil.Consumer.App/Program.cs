using Microsoft.Extensions.Configuration;
using VotingSystemBigBrotherBrasil.Consumer.App.Models.Settings;
using VotingSystemBigBrotherBrasil.Consumer.App.Services;

const string RABBITMQ = "RabbitMqSettings";

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var rabbitMqSettings = configuration.GetRequiredSection(RABBITMQ).Get<RabbitMqSettings>();

var consumer = new RabbitMqConsumerService(rabbitMqSettings);

consumer.Start();