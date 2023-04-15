using AdventureWorks.Authentication;
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
        return sinkConfiguration.Sink(new MagicOnionSink(authenticationContext, endpoint));
    }
}