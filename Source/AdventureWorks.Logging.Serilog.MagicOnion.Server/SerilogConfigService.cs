using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

/// <summary>
/// Serilog設定サービス
/// </summary>
/// <remarks>
/// DIコンテナーから利用されるため未使用警告は抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class SerilogConfigService : ServiceBase<ISerilogConfigService>, ISerilogConfigService
{
    /// <summary>
    /// Serilog設定リポジトリー
    /// </summary>
    private readonly ISerilogConfigRepository _repository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="repository"></param>
    public SerilogConfigService(ISerilogConfigRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// サーバー設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async UnaryResult<SerilogConfig> GetServerSerilogConfigAsync(string applicationName)
    {
        return await _repository.GetClientSerilogConfigAsync(new ApplicationName(applicationName));
    }
}