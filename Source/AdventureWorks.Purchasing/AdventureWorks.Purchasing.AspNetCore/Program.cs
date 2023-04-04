var builder = AdventureWorks.Hosting.MagicOnion.Server.MagicOnionServerApplicationBuilder.CreateBuilder(args);

// 購買サービスの初期化
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// リポジトリーの初期化
AdventureWorks.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
