using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    private readonly ILogRecordRepository _recordRepository;

    public LoggingService(ILogRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }

    public async UnaryResult RegisterAsync(LogRecord logRecord)
    {
        await _recordRepository.RegisterAsync(logRecord);
    }
}