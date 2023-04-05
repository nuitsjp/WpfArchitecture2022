namespace AdventureWorks.Business.Purchasing;

public interface IPurchaseOrderRepository
{
    /// <summary>
    /// 発注を登録する
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    Task RegisterAsync(PurchaseOrder purchaseOrder);
}
