
using MagicOnion;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion;

/// <summary>
/// 要再発注製品に対するクエリーサービス
/// </summary>
public interface IRequiringPurchaseProductQueryService : IService<IRequiringPurchaseProductQueryService>
{
    /// <summary>
    /// 要再発注製品を取得する。
    /// </summary>
    /// <returns></returns>
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}
