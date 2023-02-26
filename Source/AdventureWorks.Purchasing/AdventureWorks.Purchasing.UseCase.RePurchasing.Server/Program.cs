using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();       // Add this line(Grpc.AspNetCore)
builder.Services.AddMagicOnion(); // Add this line(MagicOnion.Server)

StaticCompositeResolver.Instance.Register(
    StandardResolver.Instance,
    AdventureWorks.MessagePack.CustomResolver.Instance,
    AdventureWorks.Purchasing.MessagePack.CustomResolver.Instance,
    AdventureWorks.Purchasing.MessagePack.Production.CustomResolver.Instance,
    ContractlessStandardResolver.Instance
);
MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
    .WithResolver(StaticCompositeResolver.Instance);

// Database
AdventureWorks.Database.Initializer.Initialize(builder.Services, builder.Configuration);
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder.Services);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder.Services);


var app = builder.Build();

app.UseHttpsRedirection();
app.MapMagicOnionService();

app.Run();
