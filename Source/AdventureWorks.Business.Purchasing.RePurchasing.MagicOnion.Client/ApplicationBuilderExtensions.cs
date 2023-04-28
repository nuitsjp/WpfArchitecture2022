using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;

public static class ApplicationBuilderExtensions
{
    public static void UseRePurchasingMagicOnionClient(this IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRePurchasingQuery, RePurchasingQueryClient>();
    }
}