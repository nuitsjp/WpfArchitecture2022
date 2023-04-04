using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRePurchasingQuery, RePurchasingQueryClient>();
    }
}