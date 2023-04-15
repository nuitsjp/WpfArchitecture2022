using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public record LogDto(
    string Message,
    LogEventLevel Level,
    string? Exception,
    string ApplicationType,
    string Application,
    string MachineName,
    int ProcessId,
    int ThreadId,
    string LogEvent);