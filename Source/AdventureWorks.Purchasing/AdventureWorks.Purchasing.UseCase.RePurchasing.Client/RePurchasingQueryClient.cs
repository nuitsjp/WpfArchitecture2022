using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing.Client;

public class RePurchasingQueryClient : IRePurchasingQueryService
{
    private readonly IRePurchasingQueryServiceServer _server;

    public RePurchasingQueryClient(IRePurchasingQueryServiceServer server)
    {
        _server = server;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        return await _server.GetRequiringPurchaseProductsAsync();
    }
}
