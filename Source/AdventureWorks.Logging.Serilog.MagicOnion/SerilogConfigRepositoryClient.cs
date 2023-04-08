using AdventureWorks.MagicOnion.Client;
using Microsoft.Extensions.Configuration;
using Serilog.Formatting;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class SerilogConfigRepositoryClient : ISerilogConfigRepository
{
    private readonly IMagicOnionClientFactory _clientFactory;


    public SerilogConfigRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public Task<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        throw new NotImplementedException();
    }

    public async Task<SerilogConfig> GetClientSerilogConfigAsync(string applicationName)
    {
        var service = _clientFactory.Create<ISerilogConfigService>();
        return await service.GetServerSerilogConfigAsync(applicationName);
    }
}