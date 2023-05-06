namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 発注リポジトリ
/// </summary>
public interface IPurchaseOrderRepository
{
    /// <summary>
    /// 発注を登録する
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    Task RegisterAsync(PurchaseOrder purchaseOrder);
}
