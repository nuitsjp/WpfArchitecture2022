using AdventureWorks.AspNetCore;
using AdventureWorks.AspNetCore.MagicOnion;
using AdventureWorks.Hosting;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}