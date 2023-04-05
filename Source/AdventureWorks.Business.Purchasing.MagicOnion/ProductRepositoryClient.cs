using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public class ProductRepositoryClient : IProductRepository
{
    private readonly IMagicOnionClientFactory _clientFactory;


    public ProductRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Product> GetProductByIdAsync(ProductId productId)
    {
        var server = _clientFactory.Create<IProductRepositoryServer>();
        return await server.GetProductByIdAsync(productId);
    }
}