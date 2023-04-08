using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using AdventureWorks.Database;
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

    public IHost Build(string applicationName)
    {
        // Serilogの初期化
        InitializeSerilog(Configuration, applicationName);
        LoggingAspect.Logger = new ViewModelLogger();

        // MagicOnionの初期化
        AdventureWorks.MagicOnion.Client.Initializer.Initialize(this);
        _resolvers.Insert(0, StandardResolver.Instance);
        _resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
        MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
            .WithResolver(StaticCompositeResolver.Instance);

        // 認証サービスを初期化する。
        AdventureWorks.Authentication.Jwt.Rest.Initializer.Initialize(this);

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

    public static void InitializeSerilog(IConfiguration configuration, string applicationName)
    {
        var connectionString = ConnectionStringProvider.Resolve("sa", "P@ssw0rd!");
        var minimumLevel = GetMinimumLevel(connectionString, applicationName);
        var settingString = Properties.Resources.Wpf
            .Replace("%ConnectionString%", connectionString)
            .Replace("%MinimumLevel%", minimumLevel)
            .Replace("%ApplicationName%", applicationName);
        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var cc = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(cc)
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();
    }

    // ReSharper disable UnusedParameter.Local
    private static string GetMinimumLevel(string connectionString, string applicationName)
    {
#if DEBUG
        return "Debug";
#else
            const string defaultMinimumLevel = "Information";
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                return connection.QuerySingleOrDefault<string>(@"
select
	MinimumLevel
from
	[dbo].[LogSettings]
where
	ApplicationName = @ApplicationName",
                    new
                    {
                        ApplicationName = applicationName
                    }) ?? defaultMinimumLevel;
            }
            catch
            {
                return defaultMinimumLevel;
            }
#endif
    }
}

public class ViewModelLogger : IViewModelLogger
{
    public void LogEntry(MethodBase method, object[] args)
    {
        Log.Debug("{Type}.{Method}({Args}) Entry", method.ReflectedType!.FullName, method.Name, args);
    }

    public void LogSuccess(MethodBase method, object[] args)
    {
        Log.Debug("{Type}.{Method}({Args}) Success", method.ReflectedType!.FullName, method.Name, args);
    }

    public void LogException(MethodBase method, Exception exception, object[] args)
    {
        Log.Debug(exception, "{Type}.{Method}({Args}) Exception", method.ReflectedType!.FullName, method.Name, args);
    }
}