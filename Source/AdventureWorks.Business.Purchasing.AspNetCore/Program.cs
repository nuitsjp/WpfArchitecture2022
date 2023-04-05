using AdventureWorks.Business.Purchasing.MagicOnion.Server;

var builder = AdventureWorks.Hosting.MagicOnion.Server.MagicOnionServerApplicationBuilder.CreateBuilder(args);

// 購買サービスの初期化
Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// リポジトリーの初期化
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
