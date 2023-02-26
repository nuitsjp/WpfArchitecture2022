using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing.Client;

public class RePurchasingQueryClient : IRePurchasingQueryService
{
    private readonly GrpcChannel _channel;

    public RePurchasingQueryClient(GrpcChannel channel)
    {
        _channel = channel;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = MagicOnionClient.Create<IRePurchasingQueryServiceServer>(_channel);
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
