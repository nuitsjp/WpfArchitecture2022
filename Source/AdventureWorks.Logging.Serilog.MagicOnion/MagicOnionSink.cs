using System.Diagnostics;
using AdventureWorks.Authentication;
using AdventureWorks.MagicOnion.Client;
using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class MagicOnionSink : ILogEventSink
{
    private readonly IMagicOnionClientFactory _clientFactory;
    private readonly string _hostName = System.Net.Dns.GetHostName();
    private readonly string _user;

    public MagicOnionSink(IMagicOnionClientFactory clientFactory, IAuthenticationContext authenticationContext)
    {
        _clientFactory = clientFactory;
        _user = authenticationContext.CurrentEmployee.Name;
    }

    public async void Emit(LogEvent logEvent)
    {
        try
        {
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
                    _user,
                    Environment.ProcessId,
                    Environment.CurrentManagedThreadId,
                    0));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
