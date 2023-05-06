using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;

/// <summary>
/// 初期化サービス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 再発注クライアントを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UseRePurchasingMagicOnionClient(this IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRequiringPurchaseProductQuery, RequiringPurchaseProductQueryClient>();
    }
}