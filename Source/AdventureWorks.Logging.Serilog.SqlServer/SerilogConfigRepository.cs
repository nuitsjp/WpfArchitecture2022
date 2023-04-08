using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class SerilogConfigRepository : ISerilogConfigRepository
{
    private readonly SerilogDatabase _database;

    public SerilogConfigRepository(SerilogDatabase database)
    {
        _database = database;
    }

    public async Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, "Server Default");
    }

    public async Task<SerilogConfig> GetClientSerilogConfigAsync(string applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, "Client Default");
    }

    private async Task<SerilogConfig> GetSerilogConfigAsync(string applicationName, string defaultName)
    {
        using var connection = _database.Open();

        const string query = @"
select
	ApplicationName, 
	MinimumLevel,
	Settings
from
	Serilog.vLogSettings
where
	ApplicationName = @ApplicationName";

        var config = await connection.QuerySingleOrDefaultAsync<SerilogConfig>(
            query,
            new
            {
                ApplicationName = applicationName,
            });
        if (config is not null)
        {
            return config;
        }

        return await connection.QuerySingleAsync<SerilogConfig>(
            query,
            new
            {
                ApplicationName = defaultName,
            });
    }
}