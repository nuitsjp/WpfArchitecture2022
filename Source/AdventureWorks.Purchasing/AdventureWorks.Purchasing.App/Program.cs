using System.Diagnostics;
using AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;
using AdventureWorks.Wpf.Hosting;

AppDomain.CurrentDomain.FirstChanceException += (_, eventArgs) =>
{
    Debug.WriteLine(eventArgs.Exception);
};

var builder = ApplicationBuilder<AdventureWorks.Purchasing.View.App, AdventureWorks.Purchasing.View.MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
AdventureWorks.Authentication.MagicOnion.Client.Initializer.Initialize(builder);

// 購買サービスのクライアントを初期化する。
AdventureWorks.Purchasing.MagicOnion.Client.Initializer.Initialize(builder);
Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build("AdventureWorks.Purchasing.App");
await app.RunAsync();
