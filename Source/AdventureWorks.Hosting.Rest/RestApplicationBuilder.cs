using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.SqlServer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;
using System.Text;
using MessagePack;

namespace AdventureWorks.Hosting.Rest;

/// <summary>
/// RESTサービスアプリケーションを構築する。
/// </summary>
public class RestApplicationBuilder : IApplicationBuilder
{
    /// <summary>
    /// ベースとなるWebApplicationBuilder
    /// </summary>
    protected readonly WebApplicationBuilder Builder;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="builder"></param>
    protected RestApplicationBuilder(WebApplicationBuilder builder)
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
    /// IFormatterResolverを追加する。
    /// </summary>
    /// <param name="resolver"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddFormatterResolver(IFormatterResolver resolver)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static RestApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

    /// <summary>
    /// ビルダーを構築する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task<WebApplication> BuildAsync(ApplicationName applicationName)
    {
        Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        // Serilogの初期化
        await InitializeSerilogAsync(applicationName);

        Builder.Services.AddControllers();
        Builder.Services.AddEndpointsApiExplorer();
        Builder.Services.AddSwaggerGen();

        Builder.Services
            .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        Builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

        var app = Builder.Build();
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

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
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();

        Builder.Host.UseSerilog();
    }
}