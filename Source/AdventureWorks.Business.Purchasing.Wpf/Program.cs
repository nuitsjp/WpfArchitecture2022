using System.Diagnostics;
using AdventureWorks.Business.Purchasing.View;
using Initializer = AdventureWorks.Business.Purchasing.MagicOnion.Initializer;

AppDomain.CurrentDomain.FirstChanceException += (_, eventArgs) =>
{
    Debug.WriteLine(eventArgs.Exception);
};

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<App, MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
AdventureWorks.Authentication.Jwt.Rest.Initializer.Initialize(builder);

// 購買サービスのクライアントを初期化する。
Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build("AdventureWorks.Purchasing.App");
await app.RunAsync();
