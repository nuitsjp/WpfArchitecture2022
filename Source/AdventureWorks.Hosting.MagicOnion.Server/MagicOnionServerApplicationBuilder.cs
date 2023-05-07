using System.Reflection;
using System.Text;
using AdventureWorks.Authentication;
using AdventureWorks.Authentication.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.SqlServer;
using AdventureWorks.MagicOnion.Server;
using MagicOnion.Server;
using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;


namespace AdventureWorks.Hosting.MagicOnion.Server;

/// <summary>
/// MagicOnionサーバー用のアプリケーションビルダー。
/// </summary>
public class MagicOnionServerApplicationBuilder : IMagicOnionServerApplicationBuilder
{
    /// <summary>
    /// ベースとなるWebApplicationBuilder
    /// </summary>
    protected readonly WebApplicationBuilder Builder;

    /// <summary>
    /// FormatterResolverCollection
    /// </summary>
    private readonly FormatterResolverCollection _resolvers = new();

    /// <summary>
    /// MagicOnionのサービスを提供するアセンブリーの集合
    /// </summary>
    private readonly List<Assembly> _serviceAssemblies = new();

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="builder"></param>
    public MagicOnionServerApplicationBuilder(WebApplicationBuilder builder)
    {
        Builder = builder;
    }

    /// <summary>
    /// サービス
    /// </summary>
    public IServiceCollection Services => Builder.Services;
    /// <summary>
    /// コンフィギュレーション
    /// </summary>
    public IConfiguration Configuration => Builder.Configuration;
    /// <summary>
    /// ホスト
    /// </summary>
    public IHostBuilder Host => Builder.Host;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static MagicOnionServerApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

    /// <summary>
    /// IFormatterResolverを追加する。
    /// </summary>
    /// <param name="resolver"></param>
    public void AddFormatterResolver(IFormatterResolver resolver)
    {
        _resolvers.Add(resolver);
    }

    /// <summary>
    /// MagicOnionのサービスを提供するアセンブリーを追加する。
    /// </summary>
    /// <param name="serviceAssembly"></param>
    public void AddServiceAssembly(Assembly serviceAssembly)
    {
        if (_serviceAssemblies.Contains(serviceAssembly))
        {
            return;
        }
        _serviceAssemblies.Add(serviceAssembly);
    }

    /// <summary>
    /// ビルドする。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task<WebApplication> BuildAsync(ApplicationName applicationName)
    {
        Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        // MagicOnionの初期化
        _resolvers.InitializeResolver();

        Builder.Services.AddGrpc();
        Builder.Services.AddMagicOnion(
            _serviceAssemblies.ToArray(),
            options =>
            {
                options.GlobalFilters.Add<AuthenticationFilterAttribute>();
                options.GlobalFilters.Add<LoggingFilterAttribute>();
            });

        // サーバー用認証コンテキストをDIコンテナーに登録
        Services.AddSingleton<IAuthenticationContext>(ServerAuthenticationContext.Instance);

        // Serilogの初期化
        await InitializeSerilogAsync(applicationName);

        var app = Builder.Build();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.MapMagicOnionService();

        return app;
    }

    /// <summary>
    /// Serilogを初期化する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    private async Task InitializeSerilogAsync(ApplicationName applicationName)
    {
        var database = new SerilogDatabase();
        var repository = (ISerilogConfigRepository)new SerilogConfigRepository(database);
        var config = await repository.GetServerSerilogConfigAsync(applicationName);

#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
        var minimumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%ConnectionString%", database.ConnectionString)
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", applicationName.Value);

        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        Serilog.Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationRoot)
            .Enrich.With<EmployeeIdEnricher>()
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();

        Builder.Host.UseSerilog();
    }

}