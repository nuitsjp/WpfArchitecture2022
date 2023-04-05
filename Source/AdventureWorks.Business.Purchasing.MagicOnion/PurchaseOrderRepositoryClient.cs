﻿using AdventureWorks.Business.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public class PurchaseOrderRepositoryClient : IPurchaseOrderRepository
{
    private readonly IMagicOnionClientFactory _clientFactory;

    public PurchaseOrderRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        var server = _clientFactory.Create<IPurchaseOrderRepositoryService>();
        await server.RegisterAsync(purchaseOrder);
    }
}