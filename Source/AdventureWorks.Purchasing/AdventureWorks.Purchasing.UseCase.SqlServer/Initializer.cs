using AdventureWorks.Hosting;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.SqlServer.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.UseCase.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.Database.Initializer.Initialize(builder);

        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}