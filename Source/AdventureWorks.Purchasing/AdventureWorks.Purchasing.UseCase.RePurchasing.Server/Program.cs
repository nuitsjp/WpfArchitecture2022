using AdventureWorks.Database;
using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing.Client;
using MagicOnion.Server;
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
AdventureWorks.Database.TypeHandlerInitializer.Initialize();
AdventureWorks.Purchasing.Database.TypeHandlerInitializer.Initialize();
AdventureWorks.Purchasing.Database.Production.TypeHandlerInitializer.Initialize();

var connectionString = new SqlConnectionStringBuilder
{
    DataSource = "localhost",
    UserID = "sa",
    Password = "P@ssw0rd!",
    InitialCatalog = "AdventureWorks",
    TrustServerCertificate = true
}.ToString();
builder.Services.AddTransient<IDatabase>(_ => new Database(connectionString));

// Add services to the container.
builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();

var app = builder.Build();

app.MapMagicOnionService(); // Add this line

app.Run();
