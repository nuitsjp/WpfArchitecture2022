using AdventureWorks.Business.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

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