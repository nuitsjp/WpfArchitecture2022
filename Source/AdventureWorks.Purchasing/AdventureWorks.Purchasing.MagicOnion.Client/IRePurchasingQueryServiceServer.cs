using AdventureWorks.Purchasing.UseCase.RePurchasing;
using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
