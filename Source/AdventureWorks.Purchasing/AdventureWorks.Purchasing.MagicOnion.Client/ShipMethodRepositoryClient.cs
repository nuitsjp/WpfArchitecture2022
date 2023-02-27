using AdventureWorks.MagicOnion;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public class ShipMethodRepositoryClient : IShipMethodRepository
{
    private readonly MagicOnionConfig _config;

    public ShipMethodRepositoryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        var server = MagicOnionClient.Create<IShipMethodRepositoryServer>(GrpcChannel.ForAddress(_config.Address));
        return await server.GetShipMethodsAsync();
    }
}