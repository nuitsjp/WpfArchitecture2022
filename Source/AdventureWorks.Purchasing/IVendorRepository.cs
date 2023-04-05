namespace AdventureWorks.Business.Purchasing;

public interface IVendorRepository
{
    Task<Vendor> GetVendorByIdAsync(VendorId vendorId);
}