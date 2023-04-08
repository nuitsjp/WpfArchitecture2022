namespace AdventureWorks.Logging.Serilog.Rest
{
    public interface ISerilogService
    {
        Task<SerilogConfig> GetSerilogConfigAsync();
    }
}