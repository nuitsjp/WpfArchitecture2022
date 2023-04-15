namespace AdventureWorks.Logging.Serilog.MagicOnion;

public interface ILogRecordRepository
{
    Task RegisterAsync(LogRecord logRecord);
}