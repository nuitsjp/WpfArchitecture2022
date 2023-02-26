namespace AdventureWorks.Purchasing.UseCase.RePurchasing;

public interface IRePurchasingQueryService
{
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}