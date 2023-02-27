using AdventureWorks.Purchasing.Production;
using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client.Production;

public interface IProductRepositoryServer : IService<IProductRepositoryServer>
{
    UnaryResult<Product> GetProductByIdAsync(ProductId productId);

}