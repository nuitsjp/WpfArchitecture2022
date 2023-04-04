using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public interface IVendorRepositoryService : IService<IVendorRepositoryService>
{
    UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId);
}