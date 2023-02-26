using AdventureWorks.Purchasing.UseCase.RePurchasing;
using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
