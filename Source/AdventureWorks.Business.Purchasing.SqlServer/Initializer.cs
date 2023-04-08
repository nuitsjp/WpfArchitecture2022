using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient(_ => new PurchasingDatabase("sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        builder.Services.AddTransient<IVendorRepository, VendorRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
    }
}