using System.Diagnostics;
using AdventureWorks.Serilog;
using AdventureWorks.Wpf.ViewModel;
using Kamishibai;
using MessagePack.Resolvers;
using MessagePack;
using System.Reflection;
using System.Windows;
using AdventureWorks.Hosting;
using MessageBoxButton = Kamishibai.MessageBoxButton;
using MessageBoxImage = Kamishibai.MessageBoxImage;
using MessageBoxResult = Kamishibai.MessageBoxResult;

namespace AdventureWorks.Purchasing.App.Driver;

public class ApplicationBuilder<TApplication, TWindow> : IApplicationBuilder
    where TApplication : Application
    where TWindow : Window

{
    private readonly List<IFormatterResolver> _resolvers = new();
    private readonly IWpfApplicationBuilder<TApplication, TWindow> _applicationBuilder;

    public ApplicationBuilder(IWpfApplicationBuilder<TApplication, TWindow> applicationBuilder)
    {
        _applicationBuilder = applicationBuilder;
    }

    public IServiceCollection Services => _applicationBuilder.Services;
    public IConfiguration Configuration => _applicationBuilder.Configuration;
    public IHost Build(string applicationName)
    {
        var app = _applicationBuilder.Build();

        // 未処理の例外処理をセットアップする。
        app.Startup += SetupExceptionHandler;
        return app;
    }

    public void Add(IFormatterResolver resolver)
    {
        throw new NotImplementedException();
    }

    private void SetupExceptionHandler(object? _, ApplicationStartupEventArgs<TApplication, TWindow> __)
    {
        Application.Current.DispatcherUnhandledException += (sender, args) =>
        {
            Debug.WriteLine(args.Exception);
        };
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            Debug.WriteLine(args.ExceptionObject);
            Environment.Exit(1);
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            Debug.WriteLine(args.Exception);
            args.SetObserved();
        };
    }

    public static ApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        return new(KamishibaiApplication<TApplication, TWindow>.CreateBuilder());
    }
}
