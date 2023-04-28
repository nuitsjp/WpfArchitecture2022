namespace AdventureWorks.Business.Purchasing.RePurchasing;

public interface IRequiringPurchaseProductQuery
{
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}