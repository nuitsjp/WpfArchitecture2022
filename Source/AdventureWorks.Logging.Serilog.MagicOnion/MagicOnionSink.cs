using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using Serilog.Formatting.Compact;
using AdventureWorks.MagicOnion.Client;
using Grpc.Core;
using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// SerilogでMagicOnionを利用してサーバーにログを保存するためのSkin
/// </summary>
public class MagicOnionSink : ILogEventSink
{
    /// <summary>
    /// IMagicOnionClientFactory
    /// </summary>
    public static IMagicOnionClientFactory MagicOnionClientFactory { get; set; } = new NullMagicOnionClientFactory();

    /// <summary>
    /// ホスト名
    /// </summary>
    private readonly string _hostName = System.Net.Dns.GetHostName();
    /// <summary>
    /// 構造化ログ情報をコンパクトにJSON化するフォーマッター
    /// </summary>
    private readonly CompactJsonFormatter _formatter = new();
    /// <summary>
    /// ログの最低出力レベル
    /// </summary>
    private readonly LogEventLevel _restrictedToMinimumLevel;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="restrictedToMinimumLevel"></param>
    public MagicOnionSink(LogEventLevel restrictedToMinimumLevel)
    {
        _restrictedToMinimumLevel = restrictedToMinimumLevel;
    }

    /// <summary>
    /// ログを処理する。
    /// </summary>
    /// <param name="logEvent"></param>
    public async void Emit(LogEvent logEvent)
    {
        if (logEvent.Level < _restrictedToMinimumLevel)
        {
            // 最低出力レベルよりも低いレベルのログは無視する。
            return;
        }

        try
        {
            // メッセージテンプレートをレンダリングしてメッセージを取得する。
            // 不要なダブルクォーテーションを削除する。
            var message = logEvent.MessageTemplate.Render(logEvent.Properties).Replace("\"", "");

            // 構造化ログ情報をコンパクトにJSON化する。
            await using var writer = new StringWriter();
            _formatter.Format(logEvent, writer);
            var json = writer.ToString();

            // ログをサーバーに送信する。
            var service = MagicOnionClientFactory.Create<ILoggingService>();
            await service.RegisterAsync(
                new LogDto(
                    message,
                    logEvent.Level,
                    logEvent.Exception?.StackTrace,
                    logEvent.Properties["ApplicationType"].ToString().Replace("\"", ""),
                    logEvent.Properties["Application"].ToString().Replace("\"", ""),
                    _hostName,
                    Environment.ProcessId,
                    Environment.CurrentManagedThreadId,
                    json));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    /// <summary>
    /// 初期化前にEmitが呼び出されたときにデバッグコンソールにログ出力するためのIMagicOnionClientFactory
    /// </summary>
    private class NullMagicOnionClientFactory : IMagicOnionClientFactory
    {
        /// <summary>
        /// ロガーを生成する。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>() where T : IService<T>
        {
            if (typeof(T) == typeof(ILoggingService))
            {
                return (T)(object)new DebugLoggingService();
            }

            throw new NotImplementedException("ILoggingService以外には未対応です。");
        }

        /// <summary>
        /// Debugコンソールにログ出力するロガー
        /// </summary>
        private class DebugLoggingService : ILoggingService
        {
            public ILoggingService WithOptions(CallOptions option) => this;
            public ILoggingService WithHeaders(Metadata headers) => this;
            public ILoggingService WithDeadline(DateTime deadline) => this;
            public ILoggingService WithCancellationToken(CancellationToken cancellationToken) => this;
            public ILoggingService WithHost(string host) => this;
            public async UnaryResult RegisterAsync(LogDto logRecord)
            {
                await Task.CompletedTask;
                Debug.WriteLine(logRecord);
            }
        }
    }
}