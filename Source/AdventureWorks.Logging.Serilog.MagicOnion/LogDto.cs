using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// ログのデータトランスファーオブジェクト。
/// </summary>
/// <param name="Message"></param>
/// <param name="Level"></param>
/// <param name="Exception"></param>
/// <param name="ApplicationType"></param>
/// <param name="Application"></param>
/// <param name="MachineName"></param>
/// <param name="ProcessId"></param>
/// <param name="ThreadId"></param>
/// <param name="LogEvent"></param>
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