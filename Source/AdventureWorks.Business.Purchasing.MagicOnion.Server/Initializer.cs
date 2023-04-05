using AdventureWorks.Hosting.MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddFormatterResolver(Business.MagicOnion.CustomResolver.Instance);
        builder.AddFormatterResolver(CustomResolver.Instance);
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}