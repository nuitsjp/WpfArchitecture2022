using AdventureWorks.AspNetCore;
using AdventureWorks.AspNetCore.MagicOnion;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(MagicOnionApplicationBuilder builder)
    {
        builder.Add(typeof(Initializer).Assembly);
    }
}