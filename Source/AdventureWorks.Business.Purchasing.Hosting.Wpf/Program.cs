using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using PostSharp.Aspects.Advices;

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<
    AdventureWorks.Business.Purchasing.View.App,
    AdventureWorks.Business.Purchasing.View.MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
var authenticationService = new AuthenticationService();
builder.Services.AddSingleton<IAuthenticationService>(authenticationService);
builder.Services.AddSingleton(authenticationService.Context);

// 購買サービスのクライアントを初期化する。
AdventureWorks.Business.Purchasing.MagicOnion.Initializer.Initialize(builder);    
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync(typeof(Program).Assembly.GetName().Name!);
await app.RunAsync();
