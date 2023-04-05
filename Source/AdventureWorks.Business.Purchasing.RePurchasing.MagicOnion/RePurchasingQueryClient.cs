using AdventureWorks.Business.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion;

public class RePurchasingQueryClient : IRePurchasingQuery
{
    private readonly IMagicOnionClientFactory _clientFactory;

    public RePurchasingQueryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = _clientFactory.Create<IRePurchasingQueryService>();
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
