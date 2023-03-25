using AdventureWorks.Hosting;
using AdventureWorks.Purchasing.Production;
using AdventureWorks.Purchasing.SqlServer.Production;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();
        Production.TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient(_ => new PurchasingDatabase(builder.Configuration, "sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        builder.Services.AddTransient<IVendorRepository, VendorRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
    }
}