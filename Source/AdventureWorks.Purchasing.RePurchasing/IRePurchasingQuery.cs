namespace AdventureWorks.Business.Purchasing.RePurchasing;

public interface IRePurchasingQuery
{
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}