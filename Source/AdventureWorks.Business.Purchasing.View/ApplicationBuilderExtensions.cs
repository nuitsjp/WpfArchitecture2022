using AdventureWorks.Business.Purchasing.Menu.ViewModel;
using AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;
using AdventureWorks.Business.Purchasing.View.Menu;
using AdventureWorks.Business.Purchasing.View.RePurchasing;
using AdventureWorks.Hosting;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.View;

/// <summary>
/// 初期化処理拡張メソッドクラス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 購買サービスのクライアントを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UsePurchasingView(this IApplicationBuilder builder)
    {
        // メニュー
        builder.Services.AddPresentation<MainWindow, MainViewModel>();
        builder.Services.AddPresentation<MenuPage, MenuViewModel>();

        // 再発注
        builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();
        builder.Services.AddPresentation<RePurchasingPage, RePurchasingViewModel>();

    }
}