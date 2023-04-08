using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public record SerilogConfig(
    string ApplicationName,
    LogEventLevel MinimumLevel,
    string Settings);