using System.Diagnostics;

AppDomain.CurrentDomain.FirstChanceException += (_, eventArgs) =>
{
    Debug.WriteLine(eventArgs.Exception);
};

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<AdventureWorks.Purchasing.View.App, AdventureWorks.Purchasing.View.MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
AdventureWorks.Authentication.Jwt.Rest.Initializer.Initialize(builder);

// 購買サービスのクライアントを初期化する。
AdventureWorks.Purchasing.MagicOnion.Initializer.Initialize(builder);
AdventureWorks.Purchasing.RePurchasing.MagicOnion.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build("AdventureWorks.Purchasing.App");
await app.RunAsync();
