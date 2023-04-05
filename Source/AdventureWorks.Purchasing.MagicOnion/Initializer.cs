﻿using AdventureWorks.Hosting.MagicOnion;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);

        Business.MagicOnion.Initializer.Initialize(builder);

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
        builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
        builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
    }
}