using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.MagicOnion;

public class ShipMethodRepositoryClient : IShipMethodRepository
{
    private readonly MagicOnionConfig _config;

    public ShipMethodRepositoryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        var server = MagicOnionClient.Create<IShipMethodRepositoryService>(GrpcChannel.ForAddress(_config.Address));
        return await server.GetShipMethodsAsync();
    }
}