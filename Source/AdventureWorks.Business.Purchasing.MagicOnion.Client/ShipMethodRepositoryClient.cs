using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

/// <summary>
/// 支払い方法リポジトリークライアント
/// </summary>
public class ShipMethodRepositoryClient : IShipMethodRepository
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public ShipMethodRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// 支払い方法を取得する。
    /// </summary>
    /// <returns></returns>
    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        var server = _clientFactory.Create<IShipMethodRepositoryService>();
        return await server.GetShipMethodsAsync();
    }
}