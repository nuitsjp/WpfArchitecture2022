using AdventureWorks.Hosting.MagicOnion.Server;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
    }
}