using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <summary>
/// 初期化拡張メソッド
/// </summary>
public static class MagicOnionServerApplicationBuilderExtensions
{
    /// <summary>
    /// 初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UsePurchasingMagicOnionServer(this IMagicOnionServerApplicationBuilder builder)
    {
        builder.UsePurchasingMagicOnion();
        builder.AddFormatterResolver(Business.MagicOnion.CustomResolver.Instance);
        builder.AddServiceAssembly(typeof(MagicOnionServerApplicationBuilderExtensions).Assembly);
        builder.Services.AddSingleton(PurchasingAudience.Instance);
    }
}