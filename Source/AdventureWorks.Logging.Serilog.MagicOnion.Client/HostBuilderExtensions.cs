using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

public static class HostBuilderExtensions
{
    public static void UseMagicOnionLogging(this IHostBuilder host, ApplicationName applicationName)
    {
        host.ConfigureServices((_, services) =>
        {
            services.AddSingleton(applicationName);
            services.AddTransient<ILoggingInitializer, LoggingInitializer>();
        });
        host.UseSerilog();
    }
}