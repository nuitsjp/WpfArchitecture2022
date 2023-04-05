namespace AdventureWorks.Business.Purchasing;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(ProductId productId);
}