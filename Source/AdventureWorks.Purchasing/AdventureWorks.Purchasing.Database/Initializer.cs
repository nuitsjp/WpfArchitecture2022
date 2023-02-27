using AdventureWorks.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.Database;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.Database.Initializer.Initialize(builder);

        TypeHandlerInitializer.Initialize();
        Production.TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        builder.Services.AddTransient<IVendorRepository, VendorRepository>();
    }
}