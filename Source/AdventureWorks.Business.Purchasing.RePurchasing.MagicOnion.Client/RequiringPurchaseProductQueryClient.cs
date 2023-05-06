using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;

/// <summary>
/// 要再発注製品に対するクエリーサービスクライアント
/// </summary>
public class RequiringPurchaseProductQueryClient : IRequiringPurchaseProductQuery
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public RequiringPurchaseProductQueryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// 要再発注製品を取得する。
    /// </summary>
    /// <returns></returns>
    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var client = _clientFactory.Create<IRequiringPurchaseProductQueryService>();
        return await client.GetRequiringPurchaseProductsAsync();
    }
}
