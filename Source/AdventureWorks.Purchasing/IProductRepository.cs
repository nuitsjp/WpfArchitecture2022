namespace AdventureWorks.Purchasing;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(ProductId productId);
}