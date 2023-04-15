using AdventureWorks.Authentication;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    private readonly ILogRepository _eventRepository;
    private readonly IAuthenticationContext _authenticationContext;

    public LoggingService(
        ILogRepository eventRepository, 
        IAuthenticationContext authenticationContext)
    {
        _eventRepository = eventRepository;
        _authenticationContext = authenticationContext;
    }

    public async UnaryResult RegisterAsync(LogDto logRecord)
    {
        await _eventRepository.RegisterAsync(
            new Log(
                logRecord.Message,
                logRecord.Level.ToString(),
                logRecord.Exception,
                logRecord.ApplicationType,
                logRecord.Application,
                logRecord.MachineName,
                Context.CallContext.Peer,
                _authenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
                logRecord.ProcessId,
                logRecord.ThreadId,
                logRecord.LogEvent));
    }
}