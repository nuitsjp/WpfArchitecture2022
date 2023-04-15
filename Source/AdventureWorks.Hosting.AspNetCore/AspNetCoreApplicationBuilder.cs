using System.Text;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Hosting.AspNetCore;

public abstract class AspNetCoreApplicationBuilder : IApplicationBuilder
{
    protected readonly WebApplicationBuilder Builder;

    protected AspNetCoreApplicationBuilder(WebApplicationBuilder builder)
    {
        Builder = builder;
    }

    public IServiceCollection Services => Builder.Services;
    public IConfiguration Configuration => Builder.Configuration;

    public virtual async Task<WebApplication> BuildAsync(string applicationName)
    {
        Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        // Serilogの初期化
        await InitializeSerilogAsync(applicationName);

        var app = Builder.Build();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        return app;
    }

    private async Task InitializeSerilogAsync(string applicationName)
    {
        var database = new SerilogDatabase();
        var repository = (ISerilogConfigRepository)new SerilogConfigRepository(database);
        var config = await repository.GetServerSerilogConfigAsync(applicationName);

#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
        var var maximumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%ConnectionString%", database.ConnectionString)
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", applicationName);

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