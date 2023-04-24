using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
        builder.Services.AddSingleton(LoggingAudience.Audience);
    }
}