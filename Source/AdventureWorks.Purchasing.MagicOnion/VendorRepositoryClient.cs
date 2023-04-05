﻿using AdventureWorks.Business.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public class VendorRepositoryClient : IVendorRepository
{
    private readonly MagicOnionConfig _config;

    public VendorRepositoryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        var server = MagicOnionClient.Create<IVendorRepositoryService>(GrpcChannel.ForAddress(_config.Address));
        return await server.GetVendorByIdAsync(vendorId);
    }
}