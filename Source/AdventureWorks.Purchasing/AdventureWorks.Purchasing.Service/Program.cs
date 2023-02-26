using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;

var builder = AdventureWorks.AspNetCore.ApplicationBuilder.CreateBuilder(args);

// MagicOnion
AdventureWorks.MagicOnion.Initializer.Initialize(builder);
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);

// Database
AdventureWorks.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder.Services);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder.Services);

builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();

var app = builder.Build();
app.Run();

