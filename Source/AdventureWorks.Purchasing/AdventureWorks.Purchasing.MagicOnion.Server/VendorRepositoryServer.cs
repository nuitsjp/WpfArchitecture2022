using AdventureWorks.Purchasing.MagicOnion.Client;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

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