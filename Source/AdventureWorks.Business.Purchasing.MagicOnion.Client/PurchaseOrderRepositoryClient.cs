using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

/// <summary>
/// 製品リポジトリーサービス
/// </summary>
public class PurchaseOrderRepositoryClient : IPurchaseOrderRepository
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public PurchaseOrderRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// 発注する。
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    public async Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        var server = _clientFactory.Create<IPurchaseOrderRepositoryService>();
        await server.RegisterAsync(purchaseOrder);
    }
}