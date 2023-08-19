using FluentValidation;
using RabbitMQ.Client;
using VotingSystemBigBrotherBrasil.Publisher.Models.Settings;
using VotingSystemBigBrotherBrasil.Publisher.Models.VoteModel;
using VotingSystemBigBrotherBrasil.Publisher.Services;

namespace VotingSystemBigBrotherBrasil.Publisher.API.Config
{
    public static class DependencyInjectionConfig
    {
        private const string PAREDAO = "ParedaoSettings";
        private const string RABBITMQ = "RabbitMqSettings";

        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ParedaoSettings>(configuration.GetSection(PAREDAO));
            services.Configure<RabbitMqSettings>(configuration.GetSection(RABBITMQ));

            ResolveValidators(services);

            services.AddSingleton<RabbitMqPublisher>();
        }

        private static void ResolveValidators(IServiceCollection services)
        {
            services.AddSingleton<IValidator<Vote>, VoteValidator>();
        }
    }
}
