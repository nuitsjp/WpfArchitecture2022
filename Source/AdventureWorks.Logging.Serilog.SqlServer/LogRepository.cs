using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class LogRecordRepository : ILogRecordRepository
{
    private readonly SerilogDatabase _database;

    public LogRecordRepository(SerilogDatabase database)
    {
        _database = database;
    }

    public async Task RegisterAsync(LogRecord logRecord)
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
    UserName,
    ProcessId,
    ThreadId,
    CorrelationId
) values (
    @Message,
    @Level,
    getdate(),
    @Exception,
    @ApplicationType,
    @Application,
    @MachineName,
    @UserName,
    @ProcessId,
    @ThreadId,
    @CorrelationId
)
",
            logRecord);
    }
}