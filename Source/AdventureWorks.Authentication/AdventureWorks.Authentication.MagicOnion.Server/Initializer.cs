
using AdventureWorks.AspNetCore.Hosting;

namespace AdventureWorks.Authentication.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder)
    {
        AdventureWorks.MagicOnion.Initializer.Initialize(builder);
        builder.Add(typeof(Initializer).Assembly);
    }
}