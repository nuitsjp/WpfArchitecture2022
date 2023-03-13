using MagicOnion;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
