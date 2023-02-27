using AdventureWorks.Database;
using AdventureWorks.Extensions;
using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.UseCase.Database;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.Database.Initializer.Initialize(builder);

        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}