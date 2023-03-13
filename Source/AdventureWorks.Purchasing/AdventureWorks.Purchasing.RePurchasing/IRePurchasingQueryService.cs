namespace AdventureWorks.Purchasing.RePurchasing;

public interface IRePurchasingQueryService
{
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}