using AdventureWorks.Extensions;
using AdventureWorks.MagicOnion;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        AdventureWorks.MagicOnion.Initializer.Initialize(builder);
        MagicOnion.Initializer.Initialize(builder);

        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();
    }
}