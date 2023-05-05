using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public static class ApplicationBuilderExtensions
{
    public static void UsePurchasingSqlServer(this IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient(_ => new PurchasingDatabase());
        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        builder.Services.AddTransient<IVendorRepository, VendorRepository>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
    }
}