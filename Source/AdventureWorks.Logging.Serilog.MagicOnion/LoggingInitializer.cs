using System.Reflection;
using System.Security.Authentication;
using System.Text;
using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.MagicOnion.Client;
using AdventureWorks.Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class LoggingInitializer : ILoggingInitializer
{
    public static readonly Audience Audience = new("AdventureWorks.Logging");

    private readonly ApplicationName _applicationName;
    private readonly ILoggerFactory _loggerFactory;

    public LoggingInitializer(
        ApplicationName applicationName, 
        ILoggerFactory loggerFactory)
    {
        _applicationName = applicationName;
        _loggerFactory = loggerFactory;
    }

    public async Task<bool> TryInitializeAsync()
    {
        AuthenticationService authenticationService = new(new ClientAuthenticationContext(), Audience);
        var result = await authenticationService.TryAuthenticateAsync();
        if (result.IsAuthenticated is false)
        {
            throw new AuthenticationException();
        }

        var baseAddress =
            new Endpoint(
                new Uri(
                    Environments.GetEnvironmentVariable(
                        "AdventureWorks.Logging.Serilog.MagicOnion.BaseAddress",
                        "https://localhost:3001")));

        MagicOnionSink.MagicOnionClientFactory = new MagicOnionClientFactory(result.Context, baseAddress);

        var repository = new SerilogConfigRepositoryClient(MagicOnionSink.MagicOnionClientFactory);
        var config = await repository.GetClientSerilogConfigAsync(_applicationName);
#if DEBUG
        var minimumLevel = LogEventLevel.Debug;
#else
        var maximumLevel = config.MinimumLevel;
#endif
        var settingString = config.Settings
            .Replace("%MinimumLevel%", minimumLevel.ToString())
            .Replace("%ApplicationName%", _applicationName.Value);

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

        LoggingAspect.Logger = _loggerFactory.CreateLogger<LoggingAspect>();

        return true;
    }
}