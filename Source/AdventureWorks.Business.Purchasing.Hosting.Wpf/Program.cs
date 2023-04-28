using AdventureWorks.Business.Purchasing.MagicOnion;
using AdventureWorks.Business.Purchasing.MagicOnion.Client;
using AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;
using AdventureWorks.Logging.Serilog;

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<
    AdventureWorks.Business.Purchasing.View.App,
    AdventureWorks.Business.Purchasing.View.MainWindow>.CreateBuilder();

// 購買サービスのクライアントを初期化する。
builder.UsePurchasingMagicOnionClient();    
builder.UseRePurchasingMagicOnionClient();

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var applicationName = typeof(Program).Assembly.GetName().Name!;
var app = builder.Build(new ApplicationName(applicationName), PurchasingAudience.Instance);
await app.RunAsync();
