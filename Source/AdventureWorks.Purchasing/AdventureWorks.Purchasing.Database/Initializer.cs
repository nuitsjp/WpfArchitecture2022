using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.Database;

public static class Initializer
{
    public static void Initialize(IServiceCollection services)
    {
        TypeHandlerInitializer.Initialize();
        Production.TypeHandlerInitializer.Initialize();

        services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
        services.AddTransient<IVendorRepository, VendorRepository>();
    }
}