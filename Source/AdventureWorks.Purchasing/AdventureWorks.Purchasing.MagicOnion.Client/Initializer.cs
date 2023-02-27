﻿using AdventureWorks.Extensions;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.MagicOnion.Initializer.Initialize(builder);
        MagicOnion.Initializer.Initialize(builder);

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
        builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();
    }
}