using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.MagicOnion;

public class ProductRepositoryClient : IProductRepository
{
    private readonly MagicOnionConfig _config;

    public ProductRepositoryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<Product> GetProductByIdAsync(ProductId productId)
    {
        var server = MagicOnionClient.Create<IProductRepositoryServer>(GrpcChannel.ForAddress(_config.Address));
        return await server.GetProductByIdAsync(productId);
    }
}