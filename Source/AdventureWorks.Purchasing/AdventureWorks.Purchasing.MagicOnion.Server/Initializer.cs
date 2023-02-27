using AdventureWorks.AspNetCore;
using AdventureWorks.MagicOnion;
using AdventureWorks.Purchasing.MagicOnion.Client;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder)
    {
        MagicOnion.Initializer.Initialize(builder);
        builder.Add(typeof(Initializer).Assembly);

        builder.Services.AddTransient<IShipMethodRepositoryServer, ShipMethodRepositoryServer>();
        builder.Services.AddTransient<IVendorRepositoryServer, VendorRepositoryServer>();
    }
}