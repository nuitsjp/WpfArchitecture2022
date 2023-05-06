namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// ベンダーリポジトリー
/// </summary>
public interface IVendorRepository
{
    /// <summary>
    /// ベンダーを取得する。
    /// </summary>
    /// <param name="vendorId"></param>
    /// <returns></returns>
    Task<Vendor> GetVendorByIdAsync(VendorId vendorId);
}