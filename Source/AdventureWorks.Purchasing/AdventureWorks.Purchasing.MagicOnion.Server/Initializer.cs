using AdventureWorks.AspNetCore;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder)
    {
        MagicOnion.Initializer.Initialize(builder);
        builder.Add(typeof(Initializer).Assembly);
    }
}