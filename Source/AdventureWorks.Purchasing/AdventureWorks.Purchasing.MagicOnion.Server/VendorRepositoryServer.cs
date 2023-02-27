using AdventureWorks.Purchasing.MagicOnion.Client;
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
public class VendorRepositoryServer : ServiceBase<IVendorRepositoryServer>, IVendorRepositoryServer
{
    private readonly IVendorRepository _repository;

    public VendorRepositoryServer(IVendorRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        return await _repository.GetVendorByIdAsync(vendorId);
    }
}