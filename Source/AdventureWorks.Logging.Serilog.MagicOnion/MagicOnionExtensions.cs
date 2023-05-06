using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// MagicOnionSinkをコンフィギュレーションで利用するためのExtensions
/// </summary>
public static class MagicOnionExtensions
{
    public static LoggerConfiguration MagicOnion(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        LogEventLevel restrictedToMinimumLevel)
    {
        return loggerSinkConfiguration.Sink(new MagicOnionSink(restrictedToMinimumLevel));
    }
}