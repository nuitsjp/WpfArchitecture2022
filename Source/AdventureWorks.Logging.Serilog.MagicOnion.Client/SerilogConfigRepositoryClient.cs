using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

/// <summary>
/// ISerilogConfigRepositoryクライアント
/// </summary>
public class SerilogConfigRepositoryClient : ISerilogConfigRepository
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    private readonly IMagicOnionClientFactory _clientFactory;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="clientFactory"></param>
    public SerilogConfigRepositoryClient(IMagicOnionClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// サーバーのSerilog設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<SerilogConfig> GetServerSerilogConfigAsync(ApplicationName applicationName)
    {
        // クライアントで利用するISerilogConfigRepositoryクライアントのため未実装。
        throw new NotImplementedException();
    }

    /// <summary>
    /// クライアントのSerilog設定を取得する。
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task<SerilogConfig> GetClientSerilogConfigAsync(ApplicationName applicationName)
    {
        var service = _clientFactory.Create<ISerilogConfigService>();
        return await service.GetServerSerilogConfigAsync(applicationName.Value);
    }
}