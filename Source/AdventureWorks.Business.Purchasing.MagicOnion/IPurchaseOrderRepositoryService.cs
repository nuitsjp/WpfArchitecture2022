using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// 発注リポジトリーサービス
/// </summary>
public interface IPurchaseOrderRepositoryService : IService<IPurchaseOrderRepositoryService>
{
    /// <summary>
    /// 発注する。
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    UnaryResult RegisterAsync(PurchaseOrder purchaseOrder);
}