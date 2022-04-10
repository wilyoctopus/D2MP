using D2MP.Infrastructure.Interfaces;

namespace D2MP.Infrastructure
{
    internal class ConfigurationProvider : IConfigurationProvider
    {
        public string GetDatabaseConnectionString()
        {
            return Environment.GetEnvironmentVariable(Constants.EnvDatabaseConnectionStringKey);    
        }

        public string GetSteamApiSecret()
        {
            return Environment.GetEnvironmentVariable(Constants.EnvSteamApiSecretKey);
        }

        public string GetBasicAuthSecret()
        {
            return Environment.GetEnvironmentVariable(Constants.EnvBasicAuthSecretKey);
        }
    }
}