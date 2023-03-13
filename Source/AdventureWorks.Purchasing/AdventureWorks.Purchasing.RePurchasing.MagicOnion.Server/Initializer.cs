using AdventureWorks.AspNetCore.Hosting;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder)
    {
        builder.Add(typeof(Initializer).Assembly);
    }
}