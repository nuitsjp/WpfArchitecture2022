namespace AdventureWorks.Authentication.Jwt.Rest.Client;

/// <summary>
/// 認証サービスのクライアント実装
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    /// <summary>
    /// クライアント認証コンテキスト
    /// </summary>
    private readonly ClientAuthenticationContext _context;

    /// <summary>
    /// 認証対象の受信者
    /// </summary>
    private readonly Audience _audience;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="audience"></param>
    public AuthenticationService(
        ClientAuthenticationContext context, 
        Audience audience)
    {
        _context = context;
        _audience = audience;
    }

    /// <summary>
    /// 認証を試行する。
    /// </summary>
    /// <returns></returns>
    public async Task<AuthenticateResult> TryAuthenticateAsync()
    {
        try
        {
            var baseAddress = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.BaseAddress",
                "https://localhost:4001");
            var token = await HttpClient.GetStringAsync($"{baseAddress}/Authentication/{_audience.Value}");
            _context.CurrentTokenString = token;
            _context.CurrentUser = UserSerializer.Deserialize(token, _audience);
            return new(true, _context);
        }
        catch
        {
            // 認証に失敗した場合は、認証コンテキストを初期化して、認証失敗を返す。
            return new(false, _context);
        }
    }
}