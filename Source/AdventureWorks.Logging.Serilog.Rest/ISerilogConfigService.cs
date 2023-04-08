namespace AdventureWorks.Logging.Serilog.Rest;

public interface ISerilogConfigService
{
    Task<SerilogConfig> GetSerilogConfigAsync(string applicationName);
}