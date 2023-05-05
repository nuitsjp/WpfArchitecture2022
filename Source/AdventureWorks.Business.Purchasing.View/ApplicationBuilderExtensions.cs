using AdventureWorks.Business.Purchasing.Menu.ViewModel;
using AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;
using AdventureWorks.Business.Purchasing.View.Menu;
using AdventureWorks.Business.Purchasing.View.RePurchasing;
using AdventureWorks.Hosting;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.View;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        // メニュー
        builder.Services.AddPresentation<MainWindow, MainViewModel>();
        builder.Services.AddPresentation<MenuPage, MenuViewModel>();

        // 再発注
        builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();
        builder.Services.AddPresentation<RePurchasingPage, RePurchasingViewModel>();

    }
}