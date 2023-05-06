using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

/// <summary>
/// MagicOnionクライアントを初期化するための拡張メソッドクラス
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// MagicOnionクライアントを初期化するための拡張メソッド
    /// </summary>
    /// <param name="host"></param>
    /// <param name="applicationName"></param>
    public static void UseMagicOnionLogging(this IHostBuilder host, ApplicationName applicationName)
    {
        host.ConfigureServices((_, services) =>
        {
            services.AddSingleton(applicationName);
            services.AddTransient<ILoggingInitializer, LoggingInitializer>();
        });
        host.ConfigureLogging(builder =>
        {
            builder.AddProvider(new LoggerProviderProxy());
        });
    }
}