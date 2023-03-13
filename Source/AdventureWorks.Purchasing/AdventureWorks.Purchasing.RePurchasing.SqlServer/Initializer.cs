using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.RePurchasing.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.Database.Initializer.Initialize(builder);

        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}