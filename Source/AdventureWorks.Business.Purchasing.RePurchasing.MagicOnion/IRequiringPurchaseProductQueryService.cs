
using MagicOnion;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion;

public interface IRequiringPurchaseProductQueryService : IService<IRequiringPurchaseProductQueryService>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
