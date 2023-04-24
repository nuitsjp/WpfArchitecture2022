//using AdventureWorks.MagicOnion.Client;

//namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

//public class SerilogConfigRepositoryClient : ISerilogConfigRepository
//{
//    private readonly IMagicOnionClientFactory _clientFactory;


//    public SerilogConfigRepositoryClient(IMagicOnionClientFactory clientFactory)
//    {
//        _clientFactory = clientFactory;
//    }

//    public Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName)
//    {
//        throw new NotImplementedException();
//    }

//    public async Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName)
//    {
//        var service = _clientFactory.Create<ISerilogConfigService>();
//        return await service.GetServerSerilogConfigAsync(applicationName.Value);
//    }
//}