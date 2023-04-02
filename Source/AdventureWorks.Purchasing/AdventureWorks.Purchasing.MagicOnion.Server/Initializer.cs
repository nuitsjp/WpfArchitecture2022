using AdventureWorks.AspNetCore;
using AdventureWorks.AspNetCore.MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(MagicOnionApplicationBuilder builder)
    {
        MagicOnion.Initializer.Initialize(builder);
        builder.Add(typeof(Initializer).Assembly);
    }
}