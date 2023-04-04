namespace AdventureWorks.Purchasing;

public interface IVendorRepository
{
    Task<Vendor> GetVendorByIdAsync(VendorId vendorId);
}