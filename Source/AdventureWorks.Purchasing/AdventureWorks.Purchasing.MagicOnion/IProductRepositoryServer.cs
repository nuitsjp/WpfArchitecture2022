using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public interface IProductRepositoryServer : IService<IProductRepositoryServer>
{
    UnaryResult<Product> GetProductByIdAsync(ProductId productId);

}