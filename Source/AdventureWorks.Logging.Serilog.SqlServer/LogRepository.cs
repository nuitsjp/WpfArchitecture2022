using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class LogRepository : ILogRepository
{
    private readonly SerilogDatabase _database;

    public LogRepository(SerilogDatabase database)
    {
        _database = database;
    }

    public async Task RegisterAsync(Log log)
    {
        using var connection = _database.Open();
        await connection.ExecuteAsync(@"
insert into
    Serilog.Logs
(
    Message,
    Level,
    TimeStamp,
    Exception,
    ApplicationType,
    Application,
    MachineName,
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
    @EmployeeId,
    @ProcessId,
    @ThreadId,
    @LogEvent
)
",
            log);
    }
}