using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class VendorRepositoryService : ServiceBase<IVendorRepositoryService>, IVendorRepositoryService
{
    private readonly IVendorRepository _repository;

    public VendorRepositoryService(IVendorRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        return await _repository.GetVendorByIdAsync(vendorId);
    }
}