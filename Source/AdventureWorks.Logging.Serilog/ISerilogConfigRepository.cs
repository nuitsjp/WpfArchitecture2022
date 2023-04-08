using Serilog.Core;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public interface ISerilogConfigRepository
{
    Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName);
    Task<SerilogConfig> GetClientSerilogConfigAsync(string applicationName);
}