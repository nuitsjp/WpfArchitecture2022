namespace AdventureWorks.Logging.Serilog;

/// <summary>
/// ログリポジトリー。
/// </summary>
public interface ILogRepository
{
    /// <summary>
    /// ログを登録する。
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    Task RegisterAsync(Log log);
}