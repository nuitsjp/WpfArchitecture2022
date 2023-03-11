﻿using AdventureWorks.Authentication;
using AdventureWorks.Purchasing;
using AdventureWorks.Purchasing.App.Driver;
using AdventureWorks.Purchasing.App.Driver.Authentication;
using AdventureWorks.Purchasing.App.Driver.Purchasing;
using AdventureWorks.Purchasing.Production;
using AdventureWorks.Purchasing.UseCase.RePurchasing;

var builder = ApplicationBuilder<AdventureWorks.Purchasing.View.App, AdventureWorks.Purchasing.View.MainWindow>.CreateBuilder();

// 認証サービスを初期化する。
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

// 購買サービスのクライアントを初期化する。
builder.Services.AddTransient<IShipMethodRepository, ShipMethodRepositoryClient>();
builder.Services.AddTransient<IVendorRepository, VendorRepositoryClient>();
builder.Services.AddTransient<IProductRepository, ProductRepositoryClient>();
builder.Services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepositoryClient>();
builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryClient>();

// View & ViewModelを初期化する。
AdventureWorks.Purchasing.View.Initializer.Initialize(builder);

// アプリケーションをビルドし実行する。
var app = builder.Build("AdventureWorks.Purchasing.App");
await app.RunAsync();