using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class SerilogConfigRepository : ISerilogConfigRepository
{
    private readonly SerilogDatabase _database;

    static SerilogConfigRepository()
    {
        ApplicationBuilderExtensions.InitializeTypeHandler();
    }

    public SerilogConfigRepository(SerilogDatabase database)
    {
        SqlMapper.AddTypeHandler(new LogEventLevelTypeHandler());
        SqlMapper.AddTypeHandler(new ApplicationNameTypeHandler());
        _database = database;
    }

    public async Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, new ApplicationName("Server Default"));
    }

    public async Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, new ApplicationName("Client Default"));
    }

    private async Task<SerilogConfig> GetSerilogConfigAsync(
        ApplicationName applicationName, 
        ApplicationName defaultName)
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
	ApplicationName = @Value";

        return await connection.QuerySingleOrDefaultAsync<SerilogConfig>(query, applicationName) 
               ?? await connection.QuerySingleAsync<SerilogConfig>(query, defaultName);
    }
}