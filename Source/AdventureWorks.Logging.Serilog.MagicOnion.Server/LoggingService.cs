using MagicOnion;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    private readonly ILogRecordRepository _recordRepository;
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(
        ILogRecordRepository recordRepository, 
        ILogger<LoggingService> logger)
    {
        _recordRepository = recordRepository;
        _logger = logger;
    }

    public async UnaryResult RegisterAsync(LogRecord logRecord)
    {
        await _recordRepository.RegisterAsync(logRecord);
    }
}