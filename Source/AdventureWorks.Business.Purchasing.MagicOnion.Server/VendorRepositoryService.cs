using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <summary>
/// ベンダーリポジトリーサービス
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class VendorRepositoryService : ServiceBase<IVendorRepositoryService>, IVendorRepositoryService
{
    /// <summary>
    /// ベンダーリポジトリー
    /// </summary>
    private readonly IVendorRepository _repository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="repository"></param>
    public VendorRepositoryService(IVendorRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// ベンダーを取得する。
    /// </summary>
    /// <param name="vendorId"></param>
    /// <returns></returns>
    public async UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        return await _repository.GetVendorByIdAsync(vendorId);
    }
}