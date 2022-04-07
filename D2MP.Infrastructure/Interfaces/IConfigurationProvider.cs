namespace D2MP.Infrastructure.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetDatabaseConnectionString();
        string GetSteamApiSecret();
    }
}
