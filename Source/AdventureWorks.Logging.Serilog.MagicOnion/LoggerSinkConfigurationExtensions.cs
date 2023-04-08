﻿using AdventureWorks.Authentication;
using AdventureWorks.MagicOnion.Client;
using Serilog;
using Serilog.Configuration;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public static class LoggerSinkConfigurationExtensions
{
    public static LoggerConfiguration MagicOnion(
        this LoggerSinkConfiguration sinkConfiguration,
        IAuthenticationContext authenticationContext,
        string endpoint)
    {
        var sink = new MagicOnionSink(new MagicOnionClientFactory(authenticationContext, endpoint), authenticationContext);

        return sinkConfiguration.Sink(sink);
    }
}