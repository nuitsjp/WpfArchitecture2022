using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public interface IProductRepositoryServer : IService<IProductRepositoryServer>
{
    UnaryResult<Product> GetProductByIdAsync(ProductId productId);

}