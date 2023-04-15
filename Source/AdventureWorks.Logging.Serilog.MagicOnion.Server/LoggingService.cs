using AdventureWorks.Authentication;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    private readonly ILogRepository _repository;
    private readonly IAuthenticationContext _authenticationContext;

    public LoggingService(
        ILogRepository repository, 
        IAuthenticationContext authenticationContext)
    {
        _repository = repository;
        _authenticationContext = authenticationContext;
    }

    public async UnaryResult RegisterAsync(LogDto logRecord)
    {
        await _repository.RegisterAsync(
            new Log(
                logRecord.Message,
                logRecord.Level,
                logRecord.Exception,
                logRecord.ApplicationType,
                logRecord.Application,
                logRecord.MachineName,
                _authenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
                logRecord.ProcessId,
                logRecord.ThreadId,
                logRecord.LogEvent));
    }
}