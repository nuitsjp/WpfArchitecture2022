using AdventureWorks.Authentication;
using MagicOnion;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    private readonly ILogRecordRepository _recordRepository;
    private readonly IAuthenticationContext _authenticationContext;

    public LoggingService(
        ILogRecordRepository recordRepository, 
        IAuthenticationContext authenticationContext)
    {
        _recordRepository = recordRepository;
        _authenticationContext = authenticationContext;
    }

    public async UnaryResult RegisterAsync(LogRecordDto logRecord)
    {
        await _recordRepository.RegisterAsync(
            new LogRecord(
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