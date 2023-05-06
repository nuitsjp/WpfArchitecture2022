using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

/// <summary>
/// ロギングサービス
/// </summary>
public interface ILoggingService : IService<ILoggingService>
{
    /// <summary>
    /// ログを登録する。
    /// </summary>
    /// <param name="logRecord"></param>
    /// <returns></returns>
    UnaryResult RegisterAsync(LogDto logRecord);
}