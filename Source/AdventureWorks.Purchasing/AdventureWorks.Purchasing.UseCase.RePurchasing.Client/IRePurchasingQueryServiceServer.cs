using AdventureWorks.Purchasing.Production;
using MagicOnion;
using MessagePack;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing.Client;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
