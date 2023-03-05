using ApplicationBuilder = AdventureWorks.AspNetCore.Hosting.ApplicationBuilder;

var builder = ApplicationBuilder.CreateBuilder(args);

// 認証サービスの初期化
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);

// 購買サービスの初期化
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);

// リポジトリーの初期化
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
