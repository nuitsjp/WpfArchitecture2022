using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

public static class MagicOnionServerApplicationBuilderExtensions
{
    public static void UsePurchasingMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.UsePurchasingMagicOnion();
        builder.AddFormatterResolver(Business.MagicOnion.CustomResolver.Instance);
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
        builder.Services.AddSingleton(PurchasingAudience.Instance);
    }
}