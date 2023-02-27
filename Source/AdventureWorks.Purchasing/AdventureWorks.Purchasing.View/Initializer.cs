using AdventureWorks.Extensions;
using AdventureWorks.Purchasing.View.Menu;
using AdventureWorks.Purchasing.View.RePurchasing;
using AdventureWorks.Purchasing.ViewModel.Menu;
using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using AdventureWorks.Purchasing.ViewModel;
using Kamishibai;

namespace AdventureWorks.Purchasing.View;

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