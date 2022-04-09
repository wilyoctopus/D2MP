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

        public async Task<PartialMatchResult[]> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryAsync<PartialMatchResult>("SELECT * FROM partial_match_results");
                return q.ToArray();
            }
        }

        public async Task InsertAsync(PartialMatchResult match)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO partial_match_results (radiant_hero_ids, dire_hero_ids, radiant_won, match_seq_number) " +
                                              "VALUES (@radiant_hero_ids, @dire_hero_ids, @radiant_won, @match_seq_number);", 
                                              new { radiant_hero_ids = match.RadiantHeroIds, 
                                                    dire_hero_ids = match.DireHeroIds,
                                                    radiant_won = match.RadiantWon,
                                                    match_seq_number = match.MatchSeqNumber
                                              });
            }
        }

        public async Task<long> GetLastMatchSeqIdAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryFirstAsync<long>("SELECT MAX(match_seq_number) FROM partial_match_results");
                return q;
            }
        }

        public long GetLastMatchSeqId()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = connection.QueryFirst<long>("SELECT MAX(match_seq_number) FROM partial_match_results");
                return q;
            }
        }

        public async Task<int> CountAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var q = await connection.QueryFirstAsync<int>("SELECT COUNT(*) FROM partial_match_results");
                return q;
            }
        }
    }
}
