using Microsoft.Extensions.Configuration;
using VotingSystemBigBrotherBrasil.Consumer.App.Models.Settings;

const string RABBITMQ = "RabbitMqSettings";

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var rabbitMqSettings = configuration.GetRequiredSection(RABBITMQ).Get<RabbitMqSettings>();

