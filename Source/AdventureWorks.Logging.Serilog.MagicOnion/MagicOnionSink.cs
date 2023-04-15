using AdventureWorks.Authentication;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;
using Serilog;
using System.Diagnostics;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Serilog.Formatting.Compact;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class MagicOnionSink : ILogEventSink
{
    public static IMagicOnionClientFactory MagicOnionClientFactory { get; set; } = new NullMagicOnionClientFactory();
    public static IAuthenticationContext AuthenticationContext { get; set; } = default!;

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
            await using var writer = new System.IO.StringWriter();
            _formatter.Format(logEvent, writer);
            var json = writer.ToString();

            var message = logEvent.MessageTemplate.Render(logEvent.Properties).Replace("\"", "");
            if (MagicOnionClientFactory is NullMagicOnionClientFactory)
            {
                Debug.WriteLine(message);
                return;
            }

            var loggingService = MagicOnionClientFactory.Create<ILoggingService>();
            await loggingService.RegisterAsync(
                new LogRecord(
                    message,
                    logEvent.Level,
                    logEvent.Exception?.StackTrace,
                    logEvent.Properties["ApplicationType"].ToString().Replace("\"", ""),
                    logEvent.Properties["Application"].ToString().Replace("\"", ""),
                    _hostName,
                    AuthenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
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