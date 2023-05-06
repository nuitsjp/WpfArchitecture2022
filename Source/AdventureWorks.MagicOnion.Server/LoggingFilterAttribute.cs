using AdventureWorks.Authentication;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.MagicOnion.Server;

/// <summary>
/// MagicOnionサーバーにロギング処理を追加するためのフィルター属性。
/// </summary>
public class LoggingFilterAttribute : MagicOnionFilterAttribute
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<LoggingFilterAttribute> _logger;
    /// <summary>
    /// 認証コンテキスト
    /// </summary>
    private readonly IAuthenticationContext _authenticationContext;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="authenticationContext"></param>
    public LoggingFilterAttribute(
        ILogger<LoggingFilterAttribute> logger, 
        IAuthenticationContext authenticationContext)
    {
        _logger = logger;
        _authenticationContext = authenticationContext;
    }

    /// <summary>
    /// フィルターを実行する。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        try
        {
            // サービスの呼び出しをログ出力する。
            _logger.LogInformation(
                "Method:{Method} Peer:{Peer} EmployeeId:{EmployeeId}", 
                context.CallContext.Method, 
                context.CallContext.Peer, 
                _authenticationContext.CurrentUser.EmployeeId);

            // 後続を実行する。
            return next(context);
        }
        catch (Exception e)
        {
            // サービスの呼び出しで例外が発生した場合はログ出力する。
            _logger.LogError(
                e,
                "Method:{Method} Peer:{Peer} EmployeeId:{EmployeeId}",
                context.CallContext.Method,
                context.CallContext.Peer,
                _authenticationContext.CurrentUser.EmployeeId);

            // 例外はスローしてHTTPエラーとして処理する。
            throw;
        }
    }
}