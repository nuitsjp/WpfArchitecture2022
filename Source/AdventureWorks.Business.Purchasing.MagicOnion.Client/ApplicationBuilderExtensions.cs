using AdventureWorks.Business.MagicOnion;
using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Client;

/// <summary>
/// 初期化処理サービス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 購買クライアントを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UsePurchasingMagicOnionClient(this IApplicationBuilder builder)
    {
        builder.UsePurchasingMagicOnion();
        builder.UseBusinessMagicOnion();

        builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
        builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
        builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
        builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
    }
}