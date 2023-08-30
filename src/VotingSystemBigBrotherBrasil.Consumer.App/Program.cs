using Microsoft.Extensions.Configuration;
using VotingSystemBigBrotherBrasil.Consumer.App.Models.Settings;
using VotingSystemBigBrotherBrasil.Consumer.App.Services;
using VotingSystemBigBrotherBrasil.Consumer.Data;

const string RABBITMQ = "RabbitMqSettings";

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var rabbitMqSettings = configuration.GetRequiredSection(RABBITMQ).Get<RabbitMqSettings>();

var context = new VotingSystemContext();
var consumer = new RabbitMqConsumerService(rabbitMqSettings, context);

consumer.Start();