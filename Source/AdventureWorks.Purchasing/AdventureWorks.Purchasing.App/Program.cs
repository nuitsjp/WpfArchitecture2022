using System.Windows.Controls;
using AdventureWorks.Purchasing.View;
using AdventureWorks.Purchasing.View.Page;
using AdventureWorks.Purchasing.ViewModel;
using Kamishibai;
using Microsoft.Extensions.Hosting;

// Create a builder by specifying the application and main window.
var builder = KamishibaiApplication<App, MainWindow>.CreateBuilder();

builder.Services.AddPresentation<MainWindow, MainViewModel>();
builder.Services.AddPresentation<MenuPage, MenuViewModel>();

// Build and run the application.
var app = builder.Build();
await app.RunAsync();