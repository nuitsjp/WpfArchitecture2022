using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;

public class RePurchasingQueryClient : IRePurchasingQuery
{
    private readonly MagicOnionConfig _config;


    public RePurchasingQueryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = MagicOnionClient.Create<IRePurchasingQueryService>(GrpcChannel.ForAddress(_config.Address));
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
