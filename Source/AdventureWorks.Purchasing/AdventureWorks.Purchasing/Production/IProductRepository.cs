namespace AdventureWorks.Purchasing.Production;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(ProductId productId);
}