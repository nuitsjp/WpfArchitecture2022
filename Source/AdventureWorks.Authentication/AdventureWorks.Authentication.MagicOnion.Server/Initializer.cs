using AdventureWorks.Hosting.Server;

namespace AdventureWorks.Authentication.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Add(typeof(Initializer).Assembly);
    }
}