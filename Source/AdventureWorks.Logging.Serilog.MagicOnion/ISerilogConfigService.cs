using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public interface ISerilogConfigService : IService<ISerilogConfigService>
{
    UnaryResult<SerilogConfig> GetServerSerilogConfigAsync(string applicationName);

}