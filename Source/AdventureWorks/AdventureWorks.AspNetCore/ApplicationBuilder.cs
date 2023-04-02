using System.Text;
using AdventureWorks.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventureWorks.AspNetCore;
public abstract class ApplicationBuilder
{
    protected readonly WebApplicationBuilder Builder;

    protected ApplicationBuilder(WebApplicationBuilder builder)
    {
        Builder = builder;
    }

    public IServiceCollection Services => Builder.Services;
    public IConfiguration Configuration => Builder.Configuration;

    public virtual WebApplication Build(string applicationName)
    {
        Builder.Configuration.SetBasePath(Path.GetDirectoryName(Environment.ProcessPath!)!);

        InitializeSerilog(Configuration, applicationName);

        Builder.Host.UseSerilog();

        var app = Builder.Build();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        return app;
    }

    public static void InitializeSerilog(IConfiguration configuration, string applicationName)
    {
        var connectionString = ConnectionStringProvider.Resolve(configuration, "sa", "P@ssw0rd!");
        var minimumLevel = GetMinimumLevel(connectionString, applicationName);
        var settingString = Properties.Resources.Serilog
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