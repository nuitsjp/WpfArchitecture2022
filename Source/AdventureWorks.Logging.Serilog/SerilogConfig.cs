using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public record SerilogConfig(ApplicationName ApplicationName, LogEventLevel MinimumLevel, string Settings);
