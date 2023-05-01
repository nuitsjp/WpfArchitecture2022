using AdventureWorks.Authentication.Jwt;
using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Authentication.MagicOnion.Server;

/// <summary>
/// MagicOnionのサーバーサイドでサービスに認証を追加するためのフィルター属性
/// </summary>
public class AuthenticationFilterAttribute : MagicOnionFilterAttribute
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<AuthenticationFilterAttribute> _logger;

    /// <summary>
    /// サーバーサイド認証コンテキスト
    /// </summary>
    private readonly ServerAuthenticationContext _serverAuthenticationContext;

    /// <summary>
    /// サービスの受信者
    /// </summary>
    private readonly Audience _audience;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="authenticationContext"></param>
    /// <param name="audience"></param>
    public AuthenticationFilterAttribute(
        ILogger<AuthenticationFilterAttribute> logger, 
        IAuthenticationContext authenticationContext, 
        Audience audience)
    {
        _logger = logger;
        _audience = audience;
        _serverAuthenticationContext = (ServerAuthenticationContext)authenticationContext;
    }

    /// <summary>
    /// 認証を行う。認証に成功した場合、後続のサービスを呼び出す。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        try
        {
            // 認証を行う。
            var entry = context.CallContext.RequestHeaders.Get("authorization");
            var token = entry.Value.Substring("Bearer ".Length);
            _serverAuthenticationContext.CurrentUser = UserSerializer.Deserialize(token, _audience);
            _serverAuthenticationContext.CurrentTokenString = token;
        }
        catch (Exception e)
        {
            // 認証に失敗した場合、401を返す。
            _logger.LogError(e, e.Message);
            context.CallContext.GetHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        try
        {
            // 認証に成功した場合、後続のサービスを呼び出す。
            await next(context);
        }
        finally
        {
            // 後続のサービスの呼び出しが完了したら、認証コンテキストをクリアする。
            _serverAuthenticationContext.ClearCurrentUser();
        }
    }
}