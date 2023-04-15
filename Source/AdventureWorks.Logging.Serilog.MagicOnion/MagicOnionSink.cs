using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
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
using Microsoft.Extensions.Configuration;
using Serilog.Formatting.Compact;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class MagicOnionSink : ILogEventSink
{
    private readonly IMagicOnionClientFactory _clientFactory;
    private readonly string _hostName = System.Net.Dns.GetHostName();
    private readonly IAuthenticationContext _authenticationContext;
    private readonly CompactJsonFormatter _formatter = new();
    private readonly LogEventLevel _restrictedToMinimumLevel;

    public MagicOnionSink(
        LogEventLevel restrictedToMinimumLevel,
        string environmentVariableName,
        string defaultAddress)
    {
        _restrictedToMinimumLevel = restrictedToMinimumLevel;
        var context = AuthenticationServiceClient.AuthenticateAsync().Result;
        var endpoint = Environments.GetEnvironmentVariable(environmentVariableName, defaultAddress);

        _clientFactory = new MagicOnionClientFactory(context, endpoint);
        _authenticationContext = context;
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
            var loggingService = _clientFactory.Create<ILoggingService>();
            await loggingService.RegisterAsync(
                new LogRecord(
                    message,
                    logEvent.Level,
                    logEvent.Exception?.StackTrace,
                    logEvent.Properties["ApplicationType"].ToString().Replace("\"", ""),
                    logEvent.Properties["Application"].ToString().Replace("\"", ""),
                    _hostName,
                    _authenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
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
        LogEventLevel restrictedToMinimumLevel,
        string environmentVariableName,
        string defaultAddress)
    {
        return loggerSinkConfiguration.Sink(new MagicOnionSink(restrictedToMinimumLevel, environmentVariableName, defaultAddress));
    }
}