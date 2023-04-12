﻿using System.Text;
using AdventureWorks.Authentication;
using AdventureWorks.Logging.Serilog.MagicOnion;
using AdventureWorks.MagicOnion.Client;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.Hosting.Wpf;

public static class Initializer
{
    public static async Task InitializeAsync(string applicationName, IAuthenticationContext authenticationContext)
    {
        var baseAddress = Environments.GetEnvironmentVariable(
            "AdventureWorks.Logging.Serilog.MagicOnion.BaseAddress",
            "https://localhost:3001");

        var repository = new SerilogConfigRepositoryClient(new MagicOnionClientFactory(authenticationContext, baseAddress));
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
            .WriteTo.MagicOnion(authenticationContext, baseAddress)
            .CreateLogger();
    }
}
