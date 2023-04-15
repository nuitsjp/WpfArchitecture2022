using System.Reflection;
using System.Text;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.MagicOnion.Client;
using AdventureWorks.Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class LoggingInitializer : ILoggingInitializer
{
    private readonly string _applicationName;

    public LoggingInitializer(string applicationName)
    {
        _applicationName = applicationName;
    }

    public async Task<bool> TryInitializeAsync()
    {
        AuthenticationService authenticationService = new();
        await authenticationService.TryAuthenticateAsync();

        var baseAddress =
            new Endpoint(
                new Uri(
                    Environments.GetEnvironmentVariable(
                        "AdventureWorks.Logging.Serilog.MagicOnion.BaseAddress",
                        "https://localhost:3001")));

        LoggingServiceClient.MagicOnionClientFactory = new MagicOnionClientFactory(authenticationService.Context, baseAddress);

        var repository = new SerilogConfigRepositoryClient(LoggingServiceClient.MagicOnionClientFactory);
        var config = await repository.GetClientSerilogConfigAsync(_applicationName);
#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
                var var maximumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", _applicationName);

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

        LoggingAspect.Logger = new ViewModelLogger();
        return true;
    }

    public class ViewModelLogger : IViewModelLogger
    {
        public void LogEntry(MethodBase method, object[] args)
        {
            global::Serilog.Log.Debug("{Type}.{Method}({Args}) Entry", method.ReflectedType!.FullName, method.Name, args);
        }

        public void LogSuccess(MethodBase method, object[] args)
        {
            global::Serilog.Log.Debug("{Type}.{Method}({Args}) Success", method.ReflectedType!.FullName, method.Name, args);
        }

        public void LogException(MethodBase method, Exception exception, object[] args)
        {
            global::Serilog.Log.Warning(exception, "{Type}.{Method}({Args}) Exception", method.ReflectedType!.FullName, method.Name, args);
        }
    }
}