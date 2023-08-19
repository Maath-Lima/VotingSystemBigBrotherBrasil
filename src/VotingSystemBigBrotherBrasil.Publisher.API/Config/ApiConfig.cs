using Microsoft.AspNetCore.Mvc;

namespace VotingSystemBigBrotherBrasil.Publisher.API.Config
{
    public static class ApiConfig
    {
        public static void AddApiConfig(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
