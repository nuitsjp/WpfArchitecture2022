using AdventureWorks.Authentication.Jwt.Rest.Client;
using AdventureWorks.MagicOnion.Client;
using AdventureWorks.Wpf.ViewModel;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

/// <summary>
/// クライアントロギングの初期化クラス
/// </summary>
public class LoggingInitializer : ILoggingInitializer
{
    /// <summary>
    /// アプリケーション名
    /// </summary>
    private readonly ApplicationName _applicationName;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="applicationName"></param>
    public LoggingInitializer(ApplicationName applicationName)
    {
        _applicationName = applicationName;
    }

    /// <summary>
    /// エンドポイントを取得する。
    /// </summary>
    private Endpoint Endpoint =>
        new(new Uri(
            Environments.GetEnvironmentVariable(
                "AdventureWorks.Logging.Serilog.MagicOnion.BaseAddress",
                "https://localhost:3001")));

    /// <summary>
    /// ロガーを初期化する。
    /// </summary>
    /// <returns></returns>
    public async Task<bool> TryInitializeAsync()
    {
        // ロギングドメインの認証処理を行う
        AuthenticationService authenticationService = new(new ClientAuthenticationContext(), LoggingAudience.Instance);
        var result = await authenticationService.TryAuthenticateAsync();
        if (result.IsAuthenticated is false)
        {
            return false;
        }

        // ロギング設定を取得する
        MagicOnionClientFactory factory = new(result.Context, Endpoint);
        var repository = new SerilogConfigRepositoryClient(factory);
        var config = await repository.GetClientSerilogConfigAsync(_applicationName);
#if DEBUG
        config = config with { MinimumLevel = LogEventLevel.Debug };
#endif

        // ロガーをビルドする
        var logger = config.Build();

        // ロギング設定を適用する
        MagicOnionSink.MagicOnionClientFactory = factory;
        var provider = new SerilogLoggerProvider(logger);
        LoggerProviderProxy.Provider = provider;
        LoggingAspect.Logger = provider.CreateLogger(typeof(LoggingAspect).FullName!);

        return true;
    }
}