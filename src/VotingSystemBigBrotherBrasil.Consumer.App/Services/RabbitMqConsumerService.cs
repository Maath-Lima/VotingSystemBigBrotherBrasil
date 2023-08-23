using RabbitMQ.Client;

namespace VotingSystemBigBrotherBrasil.Consumer.App.Services
{
    public class RabbitMqConsumerService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumerService()
        {
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
