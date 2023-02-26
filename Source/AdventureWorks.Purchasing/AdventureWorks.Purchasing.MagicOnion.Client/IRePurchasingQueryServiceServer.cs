using MagicOnion;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing.Client;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
