﻿using MagicOnion;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public interface ILoggingService : IService<ILoggingService>
{
    UnaryResult RegisterAsync(LogRecordDto logRecord);
}

public record LogRecordDto(
    string Message,
    LogEventLevel Level,
    string? Exception,
    string ApplicationType,
    string Application,
    string MachineName,
    int ProcessId,
    int ThreadId,
    string LogEvent);