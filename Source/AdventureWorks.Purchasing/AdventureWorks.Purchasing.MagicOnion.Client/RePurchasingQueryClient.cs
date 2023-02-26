using AdventureWorks.MagicOnion;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public class RePurchasingQueryClient : IRePurchasingQueryService
{
    private readonly MagicOnionConfig _config;


    public RePurchasingQueryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = MagicOnionClient.Create<IRePurchasingQueryServiceServer>(GrpcChannel.ForAddress(_config.Address));
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
