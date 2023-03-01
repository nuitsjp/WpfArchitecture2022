﻿using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public class PurchaseOrderRepositoryClient : IPurchaseOrderRepository
{
    private readonly MagicOnionConfig _config;

    public PurchaseOrderRepositoryClient(MagicOnionConfig config)
    {
        _config = config;
    }

    public async Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        var server = MagicOnionClient.Create<IPurchaseOrderRepositoryServer>(GrpcChannel.ForAddress(_config.Address));
        await server.RegisterAsync(purchaseOrder);
    }
}