using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// 支払い方法リポジトリーサービス
/// </summary>
public interface IShipMethodRepositoryService : IService<IShipMethodRepositoryService>
{
    /// <summary>
    /// 支払い方法を取得する。
    /// </summary>
    /// <returns></returns>
    UnaryResult<IList<ShipMethod>> GetShipMethodsAsync();
}