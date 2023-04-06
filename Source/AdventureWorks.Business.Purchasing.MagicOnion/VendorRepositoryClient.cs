using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public class VendorRepositoryClient : IVendorRepository
{
    private readonly IMagicOnionClientFactory _clientFactory;

    public VendorRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        var server = _clientFactory.Create<IVendorRepositoryService>();
        return await server.GetVendorByIdAsync(vendorId);
    }
}