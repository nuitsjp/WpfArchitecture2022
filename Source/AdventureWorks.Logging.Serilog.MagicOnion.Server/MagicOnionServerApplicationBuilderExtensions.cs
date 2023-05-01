using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

public static class MagicOnionServerApplicationBuilderExtensions
{
    public static void UseSerilogMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
        builder.Services.AddSingleton(LoggingAudience.Instance);
    }
}