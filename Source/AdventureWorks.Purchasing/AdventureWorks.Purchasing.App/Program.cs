var builder = AdventureWorks.Purchasing.View.App.CreateBuilder();

// 認証サービスを初期化する。
AdventureWorks.Authentication.MagicOnion.Client.Initializer.Initialize(builder);

// 購買サービスのクライアントを初期化する。
AdventureWorks.Purchasing.MagicOnion.Client.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build();
await app.RunAsync();
