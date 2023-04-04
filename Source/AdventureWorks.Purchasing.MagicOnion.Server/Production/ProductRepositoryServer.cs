using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server.Production;

/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class ProductRepositoryServer : ServiceBase<IProductRepositoryServer>, IProductRepositoryServer
{
    private readonly IProductRepository _repository;

    public ProductRepositoryServer(IProductRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult<Product> GetProductByIdAsync(ProductId productId)
    {
        return await _repository.GetProductByIdAsync(productId);
    }
}