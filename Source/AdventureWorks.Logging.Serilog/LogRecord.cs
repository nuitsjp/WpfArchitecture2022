using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public record LogRecord(
    string Message,
    LogEventLevel Level,
    string? Exception,
    string ApplicationType,
    string Application,
    string MachineName,
    int ProcessId,
    int ThreadId,
    int CorrelationId);

public interface ILogRecordRepository
{
    Task RegisterAsync(LogRecord logRecord);
}