using D2MP.Data.Interfaces;
using D2MP.Infrastructure.Interfaces;
using Dapper;
using FluentMigrator.Runner;
using Npgsql;

namespace D2MP.Data
{
    internal class MigrationService : IMigrationService
    {
        private readonly IMigrationRunner _migrationRunner;
        private readonly IConfigurationProvider _configurationProvider;

        public MigrationService(IMigrationRunner migrationRunner, IConfigurationProvider configurationProvider) 
        {
            _migrationRunner = migrationRunner;
            _configurationProvider = configurationProvider;
        }

        public void MigrateUp()
        {
            EnsureDatabaseCreated();
            _migrationRunner.MigrateUp();
            
        }

        private void EnsureDatabaseCreated()
        {
            // Replace database name in order to query postgres' system db
            var connString = _configurationProvider.GetDatabaseConnectionString().Replace("d2mp","postgres");
            var databasesQuery = "SELECT COUNT(*) FROM postgres.pg_catalog.pg_database where datname = @name";
            var createDatabaseQuery = "CREATE DATABASE \"{0}\"";
            
            using (var conn = new NpgsqlConnection(connString))
            {
                if (conn.QuerySingle<int>(databasesQuery, new { name = "d2mp" }) != 0)
                    return;

                var command = String.Format(createDatabaseQuery, "d2mp");
                conn.Execute(command);
            }
        }
    }
}
