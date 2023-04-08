using Serilog.Events;

namespace AdventureWorks.Logging.Serilog;

public class SerilogConfig
{
    public SerilogConfig(string applicationName, string minimumLevel, string settings)
    {
        ApplicationName = applicationName;
        MinimumLevel = minimumLevel;
        Settings = settings;
    }

    public string ApplicationName { get; init; }
    public string MinimumLevel { get; init; }
    public string Settings { get; init; }
}