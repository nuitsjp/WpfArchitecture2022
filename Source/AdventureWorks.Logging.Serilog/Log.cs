namespace AdventureWorks.Logging.Serilog;

public record Log(
    string Message,
    string Level,
    string? Exception,
    string ApplicationType,
    string Application,
    string MachineName,
    string Peer,
    int EmployeeId,
    int ProcessId,
    int ThreadId,
    string LogEvent);