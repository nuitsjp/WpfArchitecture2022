
using MagicOnion;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion;

public interface IRePurchasingQueryService : IService<IRePurchasingQueryService>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
