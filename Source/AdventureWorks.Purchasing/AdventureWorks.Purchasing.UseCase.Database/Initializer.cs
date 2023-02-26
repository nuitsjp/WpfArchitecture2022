using AdventureWorks.Database;
using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.UseCase.Database;

public static class Initializer
{
    public static void Initialize(IServiceCollection services)
    {
        TypeHandlerInitializer.Initialize();

        services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}