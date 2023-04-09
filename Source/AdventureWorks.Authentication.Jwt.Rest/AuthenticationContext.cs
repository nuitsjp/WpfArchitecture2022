using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// IAuthenticationContextのJWTによる実装。
/// </summary>
public class AuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    /// <summary>
    /// 認証サービスクライアント
    /// </summary>
    private static readonly IAuthenticationService AuthenticationService = new AuthenticationServiceClient();

    /// <summary>
    /// 認証済トークン
    /// </summary>
    private string? _tokenString;
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    private User? _currentEmployee;

    /// <summary>
    /// 認証済みトークンを取得する。
    /// </summary>
    public string CurrentTokenString
    {
        get
        {
            if (_tokenString is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _tokenString;
        }
    }

    /// <summary>
    /// 認証済ユーザーを取得する。
    /// </summary>
    public User CurrentUser
    {
        get
        {
            if (_currentEmployee is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _currentEmployee;
        }
    }

    /// <summary>
    /// 認証処理を行う。
    /// </summary>
    /// <param name="audience"></param>
    /// <returns></returns>
    public bool TryAuthenticate(string audience)
    {
        var task = AuthenticationService.AuthenticateAsync(audience);
        _tokenString = task.Result;
        _currentEmployee = UserSerializer.Deserialize(_tokenString, audience);

        return true;
    }

    /// <summary>
    /// 認証処理RESTサービスを呼び出すためのクライアント
    /// </summary>
    private class AuthenticationServiceClient : IAuthenticationService
    {
        public async Task<string> AuthenticateAsync(string audience)
        {
            var endpoint = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.Endpoint",
                "https://localhost:4001");
            return await HttpClient.GetStringAsync($"{endpoint}/Authentication/{audience}");
        }
    }
}
