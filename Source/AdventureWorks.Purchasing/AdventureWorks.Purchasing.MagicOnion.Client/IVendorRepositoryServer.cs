using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IVendorRepositoryServer : IService<IVendorRepositoryServer>
{
    UnaryResult<Vendor> GetVendorByIdAsync(VendorId vendorId);
}