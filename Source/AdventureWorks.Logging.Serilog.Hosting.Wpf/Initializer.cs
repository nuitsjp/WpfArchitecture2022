using System.Text;
using AdventureWorks.Logging.Serilog.Rest;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Hosting.Wpf;

public static class Initializer
{
    public static async Task InitializeAsync(string applicationName)
    {
        var repository = new SerilogConfigRepository();
        var config = await repository.GetClientSerilogConfigAsync(applicationName);
#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
        var var maximumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", applicationName);

        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationRoot)
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();
    }
}