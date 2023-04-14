using System.Diagnostics;
using System.Windows;
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.Rest;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.Hosting.MagicOnion;
using AdventureWorks.Wpf.ViewModel;
using Kamishibai;
using MessagePack;
using MessagePack.Resolvers;
using Serilog;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace AdventureWorks.Hosting.Wpf;

public class WpfApplicationBuilder<TApplication, TWindow> : IMagicOnionApplicationBuilder
    where TApplication : Application
    where TWindow : Window

{
    private readonly List<IFormatterResolver> _resolvers = new();
    private readonly IWpfApplicationBuilder<TApplication, TWindow> _applicationBuilder;

    public WpfApplicationBuilder(IWpfApplicationBuilder<TApplication, TWindow> applicationBuilder)
    {
        _applicationBuilder = applicationBuilder;
    }

    public IServiceCollection Services => _applicationBuilder.Services;
    public IConfiguration Configuration => _applicationBuilder.Configuration;

    public async Task<IHost> BuildAsync(string applicationName)
    {
        // 認証サービスを初期化する。
        var authenticationContext = await AuthenticationServiceClient.AuthenticateAsync(this);

        // MagicOnionの初期化
        AdventureWorks.MagicOnion.Client.Initializer.Initialize(this, _resolvers);

        // Serilogの初期化
        await Logging.Serilog.Hosting.Wpf.Initializer.InitializeAsync(applicationName, authenticationContext);
        LoggingAspect.Logger = new ViewModelLogger();

        // アプリケーションのビルド
        var app = _applicationBuilder.Build();

        // 未処理の例外処理をセットアップする。
        app.Startup += SetupExceptionHandler;
        return app;
    }

    private void SetupExceptionHandler(object? _, ApplicationStartupEventArgs<TApplication, TWindow> __)
    {
        Application.Current.DispatcherUnhandledException += (sender, args) =>
        {
            Log.Warning(args.Exception, "Dispatcher.UnhandledException sender:{Sender}", sender);
            // 例外処理の中断
            args.Handled = true;

            // システム終了確認
            var confirmResult = MessageBox.Show(
                "システムエラーが発生しました。作業を継続しますか？",
                "システムエラー",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.Yes);
            if (confirmResult == MessageBoxResult.No)
            {
                Environment.Exit(1);
            }
        };
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            Log.Warning(args.ExceptionObject as Exception, "AppDomain.UnhandledException sender:{Sender}", sender);
            
            // システム終了通知
            MessageBox.Show(
                "システムエラーが発生しました。作業を継続しますか？",
                "システムエラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK);

            Environment.Exit(1);
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            Log.Warning(args.Exception, "TaskScheduler.UnobservedTaskException sender:{Sender}", sender);
            args.SetObserved();
        };
    }

    public void AddFormatterResolver(IFormatterResolver resolver)
    {
        if (_resolvers.Contains(resolver))
        {
            return;
        }
        _resolvers.Add(resolver);
    }

    public static WpfApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        return new(KamishibaiApplication<TApplication, TWindow>.CreateBuilder());
    }
}