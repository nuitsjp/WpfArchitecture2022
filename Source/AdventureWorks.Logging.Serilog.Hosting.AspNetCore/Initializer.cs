using System.Text;
using AdventureWorks.Logging.Serilog.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AdventureWorks.Logging.Serilog.Hosting.AspNetCore;

public static class Initializer
{
    public static async Task InitializeServerAsync(string connectionString, string applicationName)
    {
        var database = new SerilogDatabase();
        //var repository = new SerilogConfigRepository(database);
        //var config = repository.GetByApplicationNameAsync(applicationName);

        var minimumLevel = GetMinimumLevel(connectionString, applicationName);
        var settingString = Properties.Resources.Server
            .Replace("%ConnectionString%", database.ConnectionString)
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