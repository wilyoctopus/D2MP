using D2MP.Data.Interfaces;
using D2MP.Infrastructure.Interfaces;
using D2MP.Models;
using Dapper;
using Npgsql;

namespace D2MP.Data.Repositories
{
    internal class HeroRepository : IHeroRepository
    {
        private readonly string _connectionString = string.Empty;

        public HeroRepository(IConfigurationProvider configurationProvider)
        {
            _connectionString = configurationProvider.GetDatabaseConnectionString();
        }

        public async Task<List<Hero>> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryAsync<Hero>("SELECT * FROM heroes");
                return q.ToList();
            }
        }
    }
}
