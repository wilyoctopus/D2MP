using D2MP.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace D2MP.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationProvider, ConfigurationProvider>();
            
            return services;
        }
    }
}
