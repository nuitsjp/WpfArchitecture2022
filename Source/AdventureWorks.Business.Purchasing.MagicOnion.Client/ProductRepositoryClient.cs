using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

/// <summary>
/// 製品リポジトリークライアント
/// </summary>
public class ProductRepositoryClient : IProductRepository
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public ProductRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// 製品を取得する。
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<Product> GetProductByIdAsync(ProductId productId)
    {
        var server = _clientFactory.Create<IProductRepositoryService>();
        return await server.GetProductByIdAsync(productId);
    }
}