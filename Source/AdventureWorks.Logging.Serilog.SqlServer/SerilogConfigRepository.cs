using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// SerilogConfigリポジトリー
/// </summary>
public class SerilogConfigRepository : ISerilogConfigRepository
{
    /// <summary>
    /// SerilogDatabase
    /// </summary>
    private readonly SerilogDatabase _database;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="database"></param>
    public SerilogConfigRepository(SerilogDatabase database)
    {
        SqlMapper.AddTypeHandler(new LogEventLevelTypeHandler());
        SqlMapper.AddTypeHandler(new ApplicationNameTypeHandler());
        _database = database;
    }

    /// <summary>
    /// サーバー設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, new ApplicationName("Server Default"));
    }

    /// <summary>
    /// クライアント設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName)
    {
        return await GetSerilogConfigAsync(applicationName, new ApplicationName("Client Default"));
    }

    /// <summary>
    /// SerilogConfigを取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <param name="defaultName"></param>
    /// <returns></returns>
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