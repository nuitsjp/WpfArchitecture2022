namespace AdventureWorks.Business.Purchasing.RePurchasing;

/// <summary>
/// 要再発注製品に対するクエリー
/// </summary>
public interface IRequiringPurchaseProductQuery
{
    /// <summary>
    /// 要再発注製品を取得する。
    /// </summary>
    /// <returns></returns>
    Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}