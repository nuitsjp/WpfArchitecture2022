using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;
using Serilog;
using System.Diagnostics;
using Serilog.Formatting.Compact;
using AdventureWorks.MagicOnion.Client;
using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class MagicOnionSink : ILogEventSink
{
    public static IMagicOnionClientFactory MagicOnionClientFactory { get; set; } = new NullMagicOnionClientFactory();

    private readonly string _hostName = System.Net.Dns.GetHostName();
    private readonly CompactJsonFormatter _formatter = new();
    private readonly LogEventLevel _restrictedToMinimumLevel;

    public MagicOnionSink(LogEventLevel restrictedToMinimumLevel)
    {
        _restrictedToMinimumLevel = restrictedToMinimumLevel;
    }

    public async void Emit(LogEvent logEvent)
    {
        if (logEvent.Level < _restrictedToMinimumLevel)
        {
            return;
        }

        try
        {
            var message = logEvent.MessageTemplate.Render(logEvent.Properties).Replace("\"", "");

            await using var writer = new StringWriter();
            _formatter.Format(logEvent, writer);
            var json = writer.ToString();

            var service = MagicOnionClientFactory.Create<ILoggingService>();
            await service.RegisterAsync(
                new LogDto(
                    message,
                    logEvent.Level,
                    logEvent.Exception?.StackTrace,
                    logEvent.Properties["ApplicationType"].ToString().Replace("\"", ""),
                    logEvent.Properties["Application"].ToString().Replace("\"", ""),
                    _hostName,
                    Environment.ProcessId,
                    Environment.CurrentManagedThreadId,
                    json));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    private class NullMagicOnionClientFactory : IMagicOnionClientFactory
    {
        public T Create<T>() where T : IService<T>
        {
            return default!;
        }
    }
}

public static class MagicOnionExtensions
{
    public static LoggerConfiguration MagicOnion(
        this LoggerSinkConfiguration loggerSinkConfiguration,
        LogEventLevel restrictedToMinimumLevel)
    {
        return loggerSinkConfiguration.Sink(new MagicOnionSink(restrictedToMinimumLevel));
    }
}