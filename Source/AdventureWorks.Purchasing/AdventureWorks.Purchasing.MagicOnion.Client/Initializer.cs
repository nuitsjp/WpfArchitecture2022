using AdventureWorks.Hosting;
using AdventureWorks.Purchasing.MagicOnion.Client.Production;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.Production;
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
        builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();
    }
}