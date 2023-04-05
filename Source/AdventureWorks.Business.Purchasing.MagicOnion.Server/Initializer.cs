using AdventureWorks.Hosting.MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        Business.Purchasing.MagicOnion.Initializer.Initialize(builder);
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}