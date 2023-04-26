using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public record SerilogConfig(ApplicationName ApplicationName, LogEventLevel MinimumLevel, string Settings)
{
    public ILogger Build()
    {
        var settingString = Settings
            .Replace("%MinimumLevel%", MinimumLevel.ToString())
            .Replace("%ApplicationName%", ApplicationName.Value);

        using var settings = new MemoryStream(Encoding.UTF8.GetBytes(settingString));
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(settings)
            .Build();

        global::Serilog.Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationRoot)
#if DEBUG
            .WriteTo.Debug()
#endif
            .CreateLogger();

        return global::Serilog.Log.Logger;
    }
}
