using AdventureWorks.Authentication;
using AdventureWorks.Business.Purchasing;
using AdventureWorks.Business.Purchasing.RePurchasing;
using AdventureWorks.Business.Purchasing.View;
using AdventureWorks.Logging;
using AdventureWorks.Purchasing.App.Driver;
using AdventureWorks.Purchasing.App.Driver.Authentication;
using AdventureWorks.Purchasing.App.Driver.Logging;
using AdventureWorks.Purchasing.App.Driver.Purchasing;

var builder = ApplicationBuilder<App, MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IAuthenticationContext, AuthenticationContext>();

// ロギングサービスを初期化する。
builder.Services.AddTransient<ILoggingInitializer, LoggingInitializer>();

// 購買サービスのクライアントを初期化する。
builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddTransient<IRePurchasingQuery, RePurchasingQuery>();

// View & ViewModelを初期化する。
Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build("AdventureWorks.Purchasing.App");
await app.RunAsync();