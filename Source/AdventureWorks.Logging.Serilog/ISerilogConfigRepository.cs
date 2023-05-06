namespace AdventureWorks.Logging.Serilog;

/// <summary>
/// Serilog設定リポジトリー
/// </summary>
public interface ISerilogConfigRepository
{
    /// <summary>
    /// サーバー設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName);

    /// <summary>
    /// クライアント設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName);
}