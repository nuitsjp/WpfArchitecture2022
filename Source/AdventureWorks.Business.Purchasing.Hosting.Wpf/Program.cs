using AdventureWorks.Business.Purchasing.MagicOnion;
using AdventureWorks.Business.Purchasing.MagicOnion.Client;
using AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;
using AdventureWorks.Business.Purchasing.View;
using AdventureWorks.Logging.Serilog;

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<App, MainWindow>.CreateBuilder();

// 購買サービスのクライアントを初期化する。
builder.UsePurchasingMagicOnionClient();    
builder.UseRePurchasingMagicOnionClient();

// View & ViewModelを初期化する。
builder.UsePurchasingView();

// アプリケーションをビルドし実行する。
var applicationName = typeof(Program).Assembly.GetName().Name!;
var app = builder.Build(new ApplicationName(applicationName), PurchasingAudience.Instance);
await app.RunAsync();
