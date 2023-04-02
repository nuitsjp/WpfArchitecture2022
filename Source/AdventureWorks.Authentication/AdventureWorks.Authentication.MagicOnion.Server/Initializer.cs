using AdventureWorks.Hosting;

namespace AdventureWorks.Authentication.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}