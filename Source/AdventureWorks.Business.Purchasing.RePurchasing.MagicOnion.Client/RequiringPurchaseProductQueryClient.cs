using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;

public class RequiringPurchaseProductQueryClient : IRequiringPurchaseProductQuery
{
    private readonly IMagicOnionClientFactory _clientFactory;

    public RequiringPurchaseProductQueryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = _clientFactory.Create<IRequiringPurchaseProductQueryService>();
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
