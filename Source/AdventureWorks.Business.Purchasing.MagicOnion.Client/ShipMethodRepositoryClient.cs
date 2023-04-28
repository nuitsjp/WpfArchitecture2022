using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

public class ShipMethodRepositoryClient : IShipMethodRepository
{
    private readonly IMagicOnionClientFactory _clientFactory;

    public ShipMethodRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        var server = _clientFactory.Create<IShipMethodRepositoryService>();
        return await server.GetShipMethodsAsync();
    }
}