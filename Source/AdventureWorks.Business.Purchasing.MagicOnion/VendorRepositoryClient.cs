using AdventureWorks.Business.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;

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