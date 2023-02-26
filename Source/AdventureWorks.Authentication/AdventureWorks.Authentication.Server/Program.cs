using AdventureWorks.Authentication.Service;
using AdventureWorks.Authentication.Service.Database;
using AdventureWorks.Database;
using MessagePack.Resolvers;
using MessagePack;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();       // Add this line(Grpc.AspNetCore)
builder.Services.AddMagicOnion(); // Add this line(MagicOnion.Server)

StaticCompositeResolver.Instance.Register(
    StandardResolver.Instance,
    AdventureWorks.MessagePack.CustomResolver.Instance,
    ContractlessStandardResolver.Instance
);
MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
    .WithResolver(StaticCompositeResolver.Instance);

// Database
TypeHandlerInitializer.Initialize();

var connectionString = new SqlConnectionStringBuilder
{
    DataSource = "localhost",
    UserID = "sa",
    Password = "P@ssw0rd!",
    InitialCatalog = "AdventureWorks",
    TrustServerCertificate = true
}.ToString();
builder.Services.AddTransient<IDatabase>(_ => new Database(connectionString));

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapMagicOnionService();

app.Run();