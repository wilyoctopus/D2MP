using D2MP.Data.Interfaces;
using D2MP.Infrastructure.Interfaces;
using D2MP.Models;
using Dapper;
using Npgsql;

namespace D2MP.Data.Repositories
{
    internal class PartialMatchResultRepository : IPartialMatchResultRepository
    {
        private readonly string _connectionString = string.Empty;

        public PartialMatchResultRepository(IConfigurationProvider configurationProvider)
        {
            _connectionString = configurationProvider.GetDatabaseConnectionString();
        }

        public async Task<PartialMatchResult[]> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryAsync<PartialMatchResult>("SELECT * FROM partial_match_results");
                return q.ToArray();
            }
        }

        public async Task Insert(PartialMatchResult match)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO partial_match_results (hero_ids, won) VALUES (@hero_ids, @won);", new { hero_ids = match.HeroIds, won = match.Won });
            }
        }

        public async Task<long> GetLastMatchSeqId()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryFirstAsync<long>("SELECT MAX(match_seq_id) FROM processed_matches");
                return q;
            }
        }

        public async Task<int> Count()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM partial_match_results");
                return q;
            }
        }

        public async Task Insert(long processedMatchSeqId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO processed_matches (match_seq_id) VALUES (@match_seq_id);", new { match_seq_id = processedMatchSeqId });
            }
        }
    }
}
