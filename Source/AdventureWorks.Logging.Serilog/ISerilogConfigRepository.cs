namespace AdventureWorks.Logging.Serilog;

public interface ISerilogConfigRepository
{
    Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName);
    Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName);
}