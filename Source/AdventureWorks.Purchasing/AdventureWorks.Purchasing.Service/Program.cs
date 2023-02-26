using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;

var builder = new AdventureWorks.AspNetCore.ApplicationBuilder(WebApplication.CreateBuilder(args));

// MagicOnion
AdventureWorks.MagicOnion.MessagePackInitializer messagePackInitializer = new();

AdventureWorks.MagicOnion.Initializer.Initialize(builder, messagePackInitializer);
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder, messagePackInitializer);

// Database
AdventureWorks.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder.Services);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder.Services);

builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();

messagePackInitializer.Initialize();
var app = builder.Build();
app.Run();

