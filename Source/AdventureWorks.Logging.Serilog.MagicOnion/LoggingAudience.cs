using AdventureWorks.Authentication.Jwt;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public static class LoggingAudience
{
    public static readonly Audience Instance = new("AdventureWorks.Logging");
}