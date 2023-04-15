using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.Logging;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.MagicOnion;
using Microsoft.Extensions.DependencyInjection;
using PostSharp.Aspects.Advices;

string applicationName = typeof(Program).Assembly.GetName().Name!;

var builder = AdventureWorks.Hosting.Wpf.WpfApplicationBuilder<
    AdventureWorks.Business.Purchasing.View.App,
    AdventureWorks.Business.Purchasing.View.MainWindow>.CreateBuilder();

// 購買サービスのクライアントを初期化する。
AdventureWorks.Business.Purchasing.MagicOnion.Initializer.Initialize(builder);    
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Initializer.Initialize(builder);

// View & ViewModelを初期化する。
AdventureWorks.Business.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync(applicationName);
await app.RunAsync();
