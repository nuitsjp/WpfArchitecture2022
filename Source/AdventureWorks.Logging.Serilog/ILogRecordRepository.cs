namespace AdventureWorks.Logging.Serilog;

public interface ILogRecordRepository
{
    Task RegisterAsync(LogRecord logRecord);
}