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
    public static IAuthenticationContext AuthenticationContext { get; set; } = default!;

    private readonly string _hostName = System.Net.Dns.GetHostName();
    private readonly CompactJsonFormatter _formatter = new();
    private readonly LogEventLevel _restrictedToMinimumLevel;
    private readonly ILoggingService _repository = new LoggingServiceClient();

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

            await _repository.RegisterAsync(
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

public class LoggingServiceClient : ILoggingService
{
    public static IMagicOnionClientFactory MagicOnionClientFactory { get; set; } = new NullMagicOnionClientFactory();

    private class NullMagicOnionClientFactory : IMagicOnionClientFactory
    {
        public T Create<T>() where T : IService<T>
        {
            return default!;
        }
    }

    public ILoggingService WithOptions(CallOptions option) => this;

    public ILoggingService WithHeaders(Metadata headers) => this;

    public ILoggingService WithDeadline(DateTime deadline) => this;

    public ILoggingService WithCancellationToken(CancellationToken cancellationToken) => this;

    public ILoggingService WithHost(string host) => this;

    public async UnaryResult RegisterAsync(LogDto logRecord)
    {
        var service = MagicOnionClientFactory.Create<ILoggingService>();
        await service.RegisterAsync(logRecord);
    }
}