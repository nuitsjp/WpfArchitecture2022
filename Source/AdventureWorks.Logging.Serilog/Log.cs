namespace AdventureWorks.Logging.Serilog;

/// <summary>
/// ログ
/// </summary>
/// <param name="Message"></param>
/// <param name="Level"></param>
/// <param name="Exception"></param>
/// <param name="ApplicationType"></param>
/// <param name="Application"></param>
/// <param name="MachineName"></param>
/// <param name="Peer"></param>
/// <param name="EmployeeId"></param>
/// <param name="ProcessId"></param>
/// <param name="ThreadId"></param>
/// <param name="LogEvent"></param>
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