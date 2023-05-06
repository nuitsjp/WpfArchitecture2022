using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.MagicOnion.Server;
using AdventureWorks.Logging.Serilog.SqlServer;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

// ロガーを初期化する。
builder.UseSerilogMagicOnionServer();
builder.UseSerilogSqlServer();

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();