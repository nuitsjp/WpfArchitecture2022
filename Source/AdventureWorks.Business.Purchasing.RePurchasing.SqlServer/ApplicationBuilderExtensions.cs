using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

/// <summary>
/// 初期化サービス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UseRePurchasingSqlServer(this IApplicationBuilder builder)
    {
        builder.Services.AddTransient(_ => new RePurchasingDatabase());
        builder.Services.AddTransient<IRequiringPurchaseProductQuery, RequiringPurchaseProductQuery>();
    }
}