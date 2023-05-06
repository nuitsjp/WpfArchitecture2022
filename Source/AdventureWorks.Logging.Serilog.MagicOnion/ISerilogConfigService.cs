using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// Serilog設定サービス
/// </summary>
public interface ISerilogConfigService : IService<ISerilogConfigService>
{
    /// <summary>
    /// サーバー設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    UnaryResult<SerilogConfig> GetServerSerilogConfigAsync(string applicationName);

}