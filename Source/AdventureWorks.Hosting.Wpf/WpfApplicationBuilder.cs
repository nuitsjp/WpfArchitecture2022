using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Authentication.Jwt.Rest;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.Hosting.MagicOnion;
using AdventureWorks.Logging;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.MagicOnion;
using AdventureWorks.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using AdventureWorks.Wpf.ViewModel;
using Kamishibai;
using MessagePack;
using MessagePack.Resolvers;
using Serilog;
using Serilog.Events;
using Log = Serilog.Log;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace AdventureWorks.Hosting.Wpf;

/// <summary>
/// AdventureWorksのWPFクライアント共通のアプリケーションビルダー
/// </summary>
/// <typeparam name="TApplication"></typeparam>
/// <typeparam name="TWindow"></typeparam>
public class WpfApplicationBuilder<TApplication, TWindow> : IMagicOnionApplicationBuilder
    where TApplication : Application
    where TWindow : Window

{
    /// <summary>
    /// MagicOnionで利用するすべてのIFormatterResolver
    /// </summary>
    private readonly FormatterResolverCollection _resolvers = new();

    /// <summary>
    /// WPFをGenericHostで動作させるためのに利用している、Wpf.Extensions.HostingのWPFアプリケーションのビルダー
    /// </summary>
    private readonly IWpfApplicationBuilder<TApplication, TWindow> _applicationBuilder;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="applicationBuilder"></param>
    public WpfApplicationBuilder(IWpfApplicationBuilder<TApplication, TWindow> applicationBuilder)
    {
        _applicationBuilder = applicationBuilder;
    }

    public IServiceCollection Services => _applicationBuilder.Services;
    public IConfiguration Configuration => _applicationBuilder.Configuration;
    private ConfigureHostBuilder Host => _applicationBuilder.Host;

    public IHost Build(ApplicationName applicationName, Audience audience)
    {
        // MagicOnionの初期化
        _resolvers.InitializeResolver();

        // MagicOnionのクライアントファクトリーをDIコンテナに登録する。
        Services.AddSingleton(GetServiceEndpoint());
        Services.AddSingleton<IMagicOnionClientFactory, MagicOnionClientFactory>();

        // 認証サービスを初期化する。
        Services.AddSingleton(audience);
        Services.AddSingleton<IAuthenticationService>(provider => new AuthenticationService(provider.GetRequiredService<Audience>()));
        Services.AddSingleton(provider => ((AuthenticationService)provider.GetRequiredService<IAuthenticationService>()).Context);

        // ロギングサービスの初期化。
        Services.AddTransient<ILoggingInitializer>(_ => new LoggingInitializer(applicationName));
        Host.UseSerilog();

        // アプリケーションのビルド
        var app = _applicationBuilder.Build();

        // 未処理の例外処理をセットアップする。
        app.Startup += SetupExceptionHandler;
        return app;
    }
    
    private static Endpoint GetServiceEndpoint() =>
        new(
            new Uri(
                Environments.GetEnvironmentVariable(
                    "AdventureWorks.Business.Purchasing.MagicOnion.BaseAddress",
                    "https://localhost:5001")));

    /// <summary>
    /// システム例外時の方針設計は、下記のブログを参照。
    /// https://zenn.dev/nuits_jp/articles/2023-03-08-wpf-unhandled-exception
    /// </summary>
    /// <param name="_"></param>
    /// <param name="__"></param>
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

    /// <summary>
    /// MagicOnionのIFormatterResolverを追加する。
    /// </summary>
    /// <param name="resolver"></param>
    public void AddFormatterResolver(IFormatterResolver resolver)
    {
        _resolvers.Add(resolver);
    }

    public static WpfApplicationBuilder<TApplication, TWindow> CreateBuilder()
    {
        return new(KamishibaiApplication<TApplication, TWindow>.CreateBuilder());
    }
}