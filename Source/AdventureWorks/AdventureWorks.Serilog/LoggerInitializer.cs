using System.Text;
using AdventureWorks.Database;
#if !DEBUG
using Dapper;
using Microsoft.Data.SqlClient;
#endif
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AdventureWorks.Serilog
{
    public static class LoggerInitializer
    {
        public static void InitializeForWpf(IConfiguration configuration, string applicationName)
        {
            Initialize(configuration, Properties.Resources.Wpf, applicationName);
        }

        private static void Initialize(IConfiguration configuration, string settingTemplate, string applicationName)
        {
            var connectionString = ConnectionStringProvider.Resolve(configuration);
            var minimumLevel = GetMinimumLevel(connectionString, applicationName);
            var settingString = settingTemplate
                .Replace("%ConnectionString%", ConnectionStringProvider.Resolve(configuration))
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
}