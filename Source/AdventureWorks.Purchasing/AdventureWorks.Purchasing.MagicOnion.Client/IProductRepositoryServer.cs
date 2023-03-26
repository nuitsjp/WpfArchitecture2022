using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IProductRepositoryServer : IService<IProductRepositoryServer>
{
    UnaryResult<Product> GetProductByIdAsync(ProductId productId);

}