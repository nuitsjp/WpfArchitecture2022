var builder = AdventureWorks.AspNetCore.ApplicationBuilder.CreateBuilder(args);

// 認証サービスの初期化
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);

// 購買サービスの初期化
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);

// リポジトリーの初期化
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder);

var app = builder.Build();
app.Run();
