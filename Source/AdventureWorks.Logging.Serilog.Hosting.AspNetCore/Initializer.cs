using System.Text;
using AdventureWorks.Logging.Serilog.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Hosting.AspNetCore;

public static class Initializer
{
    public static async Task InitializeAsync(string applicationName)
    {
        var database = new SerilogDatabase();
        var repository = new SerilogConfigRepository(database);
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