using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public interface IVendorRepositoryService : IService<IVendorRepositoryService>
{
    UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId);
}