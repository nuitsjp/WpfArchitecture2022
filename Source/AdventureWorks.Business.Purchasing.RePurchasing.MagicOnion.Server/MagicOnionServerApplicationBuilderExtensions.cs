using AdventureWorks.Hosting.MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server;

public static class MagicOnionServerApplicationBuilderExtensions
{
    public static void UseRePurchasingMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
    }
}