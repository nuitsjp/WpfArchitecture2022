using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Client;

/// <summary>
/// ロガーの設定をコンテナ起動後にロードして、ロガーの生成を切り替えることができるログプロバイダー
/// </summary>
public class LoggerProviderProxy : ILoggerProvider
{
    /// <summary>
    /// ILoggerProviderの実体。設定のロード前はDebugコンソールに出力する。
    /// </summary>
    public static ILoggerProvider Provider { get; set; } = new DebugLoggerProvider();

    /// <summary>
    /// ロガーを生成する。DI時などにコンテナーから利用される。
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return Provider.CreateLogger(categoryName);
    }

    /// <summary>
    /// リソースを解放する
    /// </summary>
    public void Dispose()
    {
        Provider.Dispose();
    }

    /// <summary>
    /// Debugコンソールに出力するロガーを生成するプロバイダー。
    /// </summary>
    private class DebugLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// ロガーを生成する。
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new DebugLogger();
        }

        /// <summary>
        /// リソースを解放する。
        /// </summary>
        public void Dispose()
        {
            // 実際に開放するリソースは存在しない。
        }
    }

    /// <summary>
    /// Debugコンソールに出力するロガー。
    /// </summary>
    private class DebugLogger : ILogger
    {
        /// <summary>
        /// スコープを開始する。
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null!;
        }

        /// <summary>
        /// 有効かどうか確認する。
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// ログ出力する
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine(formatter(state, exception));
        }
    }
}