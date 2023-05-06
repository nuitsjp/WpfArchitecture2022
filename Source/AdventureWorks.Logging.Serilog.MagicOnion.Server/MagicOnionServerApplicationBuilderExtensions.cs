using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

/// <summary>
/// 初期化拡張メソッドクラス
/// </summary>
public static class MagicOnionServerApplicationBuilderExtensions
{
    /// <summary>
    /// Serilogを利用する為の初期化を行う。
    /// </summary>
    /// <param name="builder"></param>
    public static void UseSerilogMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
        builder.Services.AddSingleton(LoggingAudience.Instance);
    }
}