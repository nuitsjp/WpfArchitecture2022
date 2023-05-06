using AdventureWorks.Authentication.Jwt;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// ロギングオーディエンス
/// </summary>
public static class LoggingAudience
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static readonly Audience Instance = new("AdventureWorks.Logging");
}