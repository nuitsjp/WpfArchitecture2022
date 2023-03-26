using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRePurchasingQuery, RePurchasingQueryClient>();
    }
}