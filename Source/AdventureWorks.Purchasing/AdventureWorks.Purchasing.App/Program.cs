
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.MagicOnion.Client;
using AdventureWorks.Purchasing;
using AdventureWorks.Purchasing.Database;
using AdventureWorks.Purchasing.View;
using AdventureWorks.Purchasing.View.Menu;
using AdventureWorks.Purchasing.View.RePurchasing;
using AdventureWorks.Purchasing.ViewModel;
using AdventureWorks.Purchasing.ViewModel.Menu;
using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using Kamishibai;

var builder = AdventureWorks.Wpf.ApplicationBuilder<App, MainWindow>.CreateBuilder();

// MagicOnion
AdventureWorks.MagicOnion.Initializer.Initialize(builder);
AdventureWorks.Purchasing.MagicOnion.Client.Initializer.Initialize(builder);

// Database
AdventureWorks.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder.Services);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder.Services);


// 認証
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

// メニュー
builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<MenuPage, MenuViewModel>();

// 再発注
builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();
builder.Services.AddPresentation<RePurchasingPage, RePurchasingViewModel>();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();


// Initialize and run the application.
var app = builder.Build();
await app.RunAsync();
