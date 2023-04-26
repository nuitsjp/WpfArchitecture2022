using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public static class Initializer
{
    public static readonly Audience Audience = new("AdventureWorks.Business.Purchasing");

    public static void Initialize(IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);

        Business.MagicOnion.Initializer.Initialize(builder);

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
        builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
        builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
    }
}