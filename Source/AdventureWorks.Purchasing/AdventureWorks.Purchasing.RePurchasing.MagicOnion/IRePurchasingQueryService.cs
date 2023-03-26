using MagicOnion;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion;

public interface IRePurchasingQueryService : IService<IRePurchasingQueryService>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
