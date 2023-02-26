
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Client;
using AdventureWorks.Purchasing;
using AdventureWorks.Purchasing.Database;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing.Client;
using AdventureWorks.Purchasing.View;
using AdventureWorks.Purchasing.View.Menu;
using AdventureWorks.Purchasing.View.RePurchasing;
using AdventureWorks.Purchasing.ViewModel;
using AdventureWorks.Purchasing.ViewModel.Menu;
using AdventureWorks.Purchasing.ViewModel.RePurchasing;
using Grpc.Net.Client;
using Kamishibai;
using MagicOnion.Client;
using MessagePack.Resolvers;
using MessagePack;
using Microsoft.Data.SqlClient;
using System.Threading.Channels;
using AdventureWorks.Database;
using AdventureWorks.Purchasing.UseCase.Database.RePurchasing;


// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

// MagicOnion
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


// 認証
builder.Services.AddSingleton<IAuthenticationService>(_ => 
    new AuthenticationService(
        MagicOnionClient.Create<IAuthenticationServiceServer>(
            GrpcChannel.ForAddress("https://localhost:5101"))));

// メニュー
builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<MenuPage, MenuViewModel>();

// 再発注
builder.Services.AddPresentation<RequiringPurchaseProductsPage, RequiringPurchaseProductsViewModel>();
builder.Services.AddPresentation<RePurchasingPage, RePurchasingViewModel>();
builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();
builder.Services.AddTransient(_ =>
    MagicOnionClient.Create<IRePurchasingQueryServiceServer>(
        GrpcChannel.ForAddress("https://localhost:5001")));
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepository>();

// Build and run the application.
var app = builder.Build();
await app.RunAsync();