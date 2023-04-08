namespace AdventureWorks.Logging.Serilog.Rest;

public interface ISerilogConfigService
{
    // ReSharper disable once UnusedMember.Global
    Task<SerilogConfig> GetSerilogConfigAsync(string applicationName);
}