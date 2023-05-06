using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// ベンダーリポジトリーサービス
/// </summary>
public interface IVendorRepositoryService : IService<IVendorRepositoryService>
{
    /// <summary>
    /// ベンダーを取得する。
    /// </summary>
    /// <param name="vendorId"></param>
    /// <returns></returns>
    UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId);
}