using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.RePurchasing.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient(_ => new RePurchasingDatabase(builder.Configuration, "sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}