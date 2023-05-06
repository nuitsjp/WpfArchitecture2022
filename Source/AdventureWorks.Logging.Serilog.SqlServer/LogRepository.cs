using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// Logリポジトリー
/// </summary>
public class LogRepository : ILogRepository
{
    /// <summary>
    /// SerilogDatabase
    /// </summary>
    private readonly SerilogDatabase _database;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="database"></param>
    public LogRepository(SerilogDatabase database)
    {
        _database = database;
    }

    /// <summary>
    /// ログを登録する。
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task RegisterAsync(Log log)
    {
        using var connection = _database.Open();
        await connection.ExecuteAsync(@"
insert into
    Serilog.vLog
(
    Message,
    Level,
    TimeStamp,
    Exception,
    ApplicationType,
    Application,
    MachineName,
    Peer,
    EmployeeId,
    ProcessId,
    ThreadId,
    LogEvent
) values (
    @Message,
    @Level,
    getdate(),
    @Exception,
    @ApplicationType,
    @Application,
    @MachineName,
    @Peer,
    @EmployeeId,
    @ProcessId,
    @ThreadId,
    @LogEvent
)
",
            log);
    }
}