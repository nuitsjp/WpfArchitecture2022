using AdventureWorks.Business.Purchasing.MagicOnion.Server;
using AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server;
using AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;
using AdventureWorks.Business.Purchasing.SqlServer;
using AdventureWorks.Business.SqlServer;
using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

// 購買サービスの初期化
builder.UsePurchasingMagicOnionServer();
builder.UseRePurchasingMagicOnionServer();

// リポジトリーの初期化
builder.UseBusinessSqlServer();
builder.UsePurchasingSqlServer();
builder.UseRePurchasingSqlServer();

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();
