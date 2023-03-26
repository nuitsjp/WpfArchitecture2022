namespace AdventureWorks.Purchasing.RePurchasing;

public interface IRePurchasingQuery
{
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}