using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion;
using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class MagicOnionSink : ILogEventSink
{
    private readonly IMagicOnionClientFactory _clientFactory;
    private readonly string _hostName = System.Net.Dns.GetHostName();
    private readonly IAuthenticationContext _authenticationContext;

    public MagicOnionSink(
        IMagicOnionClientFactory clientFactory, 
        IAuthenticationContext authenticationContext)
    {
        _clientFactory = clientFactory;
        _authenticationContext = authenticationContext;
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
                    _authenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
                    Environment.ProcessId,
                    Environment.CurrentManagedThreadId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
