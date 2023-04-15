using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public record Log(
    string Message,
    LogEventLevel Level,
    string? Exception,
    string ApplicationType,
    string Application,
    string MachineName,
    int EmployeeId,
    int ProcessId,
    int ThreadId,
    string LogEvent);