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

namespace AdventureWorks.Hosting.Rest;

public class RestApplicationBuilder : IApplicationBuilder
{
    protected readonly WebApplicationBuilder Builder;

    protected RestApplicationBuilder(WebApplicationBuilder builder)
    {
        Builder = builder;
    }

    public IServiceCollection Services => Builder.Services;
    public IConfiguration Configuration => Builder.Configuration;

    public static RestApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

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

    private async Task InitializeSerilogAsync(ApplicationName applicationName)
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