using MagicOnion.Client;

namespace AdventureWorks.Authentication.MagicOnion.Client;

/// <summary>
/// MagicOnionクライアントのようの認証フィルター
/// </summary>
public class AuthenticationFilter : IClientFilter
{
    /// <summary>
    /// 認証委コンテキスト
    /// </summary>
    private readonly IAuthenticationContext _authenticationContext;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="authenticationContext"></param>
    public AuthenticationFilter(
        IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    /// <summary>
    /// ヘッダーに認証情報を追加し、メッセージを送信する。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        // OAuth/2の仕様に合わせてヘッダーを追加する。
        var header = context.CallOptions.Headers;
        header.Add("authorization", $"Bearer {_authenticationContext.CurrentTokenString}");

        return await next(context);
    }
}