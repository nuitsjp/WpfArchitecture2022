using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

// 購買サービスの初期化
AdventureWorks.Business.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// リポジトリーの初期化
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();
