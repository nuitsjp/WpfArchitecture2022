using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IVendorRepositoryService : IService<IVendorRepositoryService>
{
    UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId);
}