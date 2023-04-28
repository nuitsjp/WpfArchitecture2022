using AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Client;
using AdventureWorks.Logging.Serilog;

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<
    AdventureWorks.Business.Purchasing.View.App,
    AdventureWorks.Business.Purchasing.View.MainWindow>.CreateBuilder();

// 購買サービスのクライアントを初期化する。
AdventureWorks.Business.Purchasing.MagicOnion.Initializer.Initialize(builder);    
builder.UseRePurchasingMagicOnionClient();

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var applicationName = typeof(Program).Assembly.GetName().Name!;
var app = builder.Build(
    new ApplicationName(applicationName),
    AdventureWorks.Business.Purchasing.MagicOnion.Initializer.Audience);
await app.RunAsync();
