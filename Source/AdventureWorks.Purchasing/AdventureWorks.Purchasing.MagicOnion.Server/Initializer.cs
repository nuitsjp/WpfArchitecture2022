using AdventureWorks.AspNetCore;
using AdventureWorks.AspNetCore.MagicOnion;
using AdventureWorks.Hosting;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        MagicOnion.Initializer.Initialize(builder);
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}