using AdventureWorks.Authentication;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Logging.Serilog.MagicOnion.Server;

/// <summary>
/// MagicOnionによるSerilogサーバー実装
/// </summary>
/// <remarks>
/// DIコンテナーから利用されるため未使用警告は抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class LoggingService : ServiceBase<ILoggingService>, ILoggingService
{
    /// <summary>
    /// Logリポジトリー
    /// </summary>
    private readonly ILogRepository _eventRepository;
    /// <summary>
    /// 認証コンテキスト
    /// </summary>
    private readonly IAuthenticationContext _authenticationContext;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="eventRepository"></param>
    /// <param name="authenticationContext"></param>
    public LoggingService(
        ILogRepository eventRepository, 
        IAuthenticationContext authenticationContext)
    {
        _eventRepository = eventRepository;
        _authenticationContext = authenticationContext;
    }

    /// <summary>
    /// ログを登録する。
    /// </summary>
    /// <param name="logRecord"></param>
    /// <returns></returns>
    public async UnaryResult RegisterAsync(LogDto logRecord)
    {
        await _eventRepository.RegisterAsync(
            new Log(
                logRecord.Message,
                logRecord.Level.ToString(),
                logRecord.Exception,
                logRecord.ApplicationType,
                logRecord.Application,
                logRecord.MachineName,
                Context.CallContext.Peer,
                _authenticationContext.CurrentUser.EmployeeId.AsPrimitive(),
                logRecord.ProcessId,
                logRecord.ThreadId,
                logRecord.LogEvent));
    }
}