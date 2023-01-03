using AdventureWorks.Purchasing.View;
using AdventureWorks.Purchasing.View.Page;
using AdventureWorks.Purchasing.View.RePurchasing;
using AdventureWorks.Purchasing.ViewModel;
using AdventureWorks.Purchasing.ViewModel.Menu;
using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using Kamishibai;

// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<MenuPage, MenuViewModel>();

builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();

// Build and run the application.
var app = builder.Build();
await app.RunAsync();