using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public interface ILoggingService : IService<ILoggingService>
{
    UnaryResult RegisterAsync(LogDto logRecord);
}