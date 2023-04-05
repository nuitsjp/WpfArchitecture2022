using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRePurchasingQuery, RePurchasingQueryClient>();
    }
}