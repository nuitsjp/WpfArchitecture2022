using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public class SerilogConfigService : ServiceBase<ISerilogConfigService>, ISerilogConfigService
{
    private readonly ISerilogConfigRepository _repository;

    public SerilogConfigService(ISerilogConfigRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        return await _repository.GetClientSerilogConfigAsync(new ApplicationName(applicationName));
    }
}