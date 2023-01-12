using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Client;
using AdventureWorks.Authentication.Service;
using AdventureWorks.Authentication.Service.Database;
using AdventureWorks.Database;
using AdventureWorks.Purchasing;
using AdventureWorks.Purchasing.Database;
using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using AdventureWorks.Purchasing.View;
using AdventureWorks.Purchasing.View.Menu;
using AdventureWorks.Purchasing.View.RePurchasing;
using AdventureWorks.Purchasing.ViewModel;
using AdventureWorks.Purchasing.ViewModel.Menu;
using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using Kamishibai;
using Microsoft.Data.SqlClient;

AdventureWorks.Database.TypeHandlerInitializer.Initialize();
AdventureWorks.Purchasing.Database.TypeHandlerInitializer.Initialize();
AdventureWorks.Purchasing.Database.Production.TypeHandlerInitializer.Initialize();

// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

// Database
var connectionString = new SqlConnectionStringBuilder
{
    DataSource = "localhost",
    UserID = "sa",
    Password = "P@ssw0rd!",
    InitialCatalog = "AdventureWorks",
    TrustServerCertificate = true
}.ToString();
builder.Services.AddTransient<IDatabase>(_ => new Database(connectionString));

// 認証
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

// メニュー
builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<MenuPage, MenuViewModel>();

// 再発注
builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();
builder.Services.AddPresentation<RePurchasingPage, RePurchasingViewModel>();
builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();

// Build and run the application.
var app = builder.Build();
await app.RunAsync();