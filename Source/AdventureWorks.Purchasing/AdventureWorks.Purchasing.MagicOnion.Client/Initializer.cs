using AdventureWorks.Extensions;
using AdventureWorks.MagicOnion;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder, MessagePackInitializer messagePackInitializer)
    {
        MagicOnion.Initializer.Initialize(builder, messagePackInitializer);

        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();
    }
}