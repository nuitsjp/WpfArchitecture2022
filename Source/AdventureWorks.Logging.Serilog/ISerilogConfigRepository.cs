namespace AdventureWorks.Logging.Serilog
{
    public interface ISerilogConfigRepository
    {
        Task<SerilogConfig> GetByApplicationNameAsync(string applicationName);
    }
}