namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 支払い方法リポジトリー
/// </summary>
public interface IShipMethodRepository
{
    /// <summary>
    /// 支払い方法を取得する。
    /// </summary>
    /// <returns></returns>
    Task<IList<ShipMethod>> GetShipMethodsAsync();
}