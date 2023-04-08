namespace AdventureWorks.Logging.Serilog
{
    public interface ISerilogConfigRepository
    {
        Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName);
    }
}