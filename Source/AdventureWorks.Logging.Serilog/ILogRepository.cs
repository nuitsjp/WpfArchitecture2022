namespace AdventureWorks.Logging.Serilog;

public interface ILogRepository
{
    Task RegisterAsync(Log log);
}