using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;

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
