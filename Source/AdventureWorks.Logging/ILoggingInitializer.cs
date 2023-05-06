namespace AdventureWorks.Logging;

/// <summary>
/// ロガーを初期化クラス。
/// </summary>
public interface ILoggingInitializer
{
    /// <summary>
    /// 初期化を行う。
    /// </summary>
    /// <returns></returns>
    Task<bool> TryInitializeAsync();
}