using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Business.MagicOnion;
using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

public static class ApplicationBuilderExtensions
{
    public static readonly Audience Audience = new("AdventureWorks.Business.Purchasing");

    public static void UsePurchasingMagicOnionClient(this IApplicationBuilder builder)
    {
        builder.UsePurchasingMagicOnion();
        builder.UseBusinessMagicOnion();

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
        builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
        builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
    }
}