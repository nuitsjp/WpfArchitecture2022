var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<
    AdventureWorks.Business.Purchasing.View.App,
    AdventureWorks.Business.Purchasing.View.MainWindow>.CreateBuilder();


// 購買サービスのクライアントを初期化する。
AdventureWorks.Business.Purchasing.MagicOnion.Initializer.Initialize(builder);    
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync("AdventureWorks.Purchasing");
await app.RunAsync();
