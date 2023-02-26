using AdventureWorks.AspNetCore;

namespace AdventureWorks.Authentication.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder)
    {
        builder.Add(typeof(Initializer).Assembly);
    }
}