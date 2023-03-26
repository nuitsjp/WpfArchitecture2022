using MagicOnion;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;

public interface IRePurchasingQueryService : IService<IRePurchasingQueryService>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
