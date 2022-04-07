using D2MP.Data.Interfaces;
using D2MP.Data.Repositories;
using D2MP.Infrastructure.Interfaces;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace D2MP.Data
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            services.AddScoped<IPartialMatchResultRepository, PartialMatchResultRepository>();
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddTransient<IMigrationService, MigrationService>();
            

            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddPostgres()
                        .WithGlobalConnectionString(GetDbConnectionString(services))
                        .ScanIn(typeof(MigrationService).Assembly).For.Migrations()
                        )
                    .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }

        private static string GetDbConnectionString(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configProvider = serviceProvider.GetService<IConfigurationProvider>();
            return configProvider.GetDatabaseConnectionString();
        }
    }
}
