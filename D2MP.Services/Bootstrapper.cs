using D2MP.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace D2MP.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddMetaPartyServices(this IServiceCollection services)
        {
            services.AddTransient<IMatchScrapingService, MatchScrapingSevice>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IStatisticsService, StatisticsService>();

            return services;
        }
    }
}
