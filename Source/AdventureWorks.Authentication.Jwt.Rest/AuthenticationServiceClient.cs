using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// 認証処理RESTサービスを呼び出すためのクライアント
/// </summary>
public class AuthenticationServiceClient
{
    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    public bool TryAuthenticate(string audience, out IClientAuthenticationContext context)
    {
        try
        {
            var endpoint = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.Endpoint",
                "https://localhost:4001");
            var token = HttpClient.GetStringAsync($"{endpoint}/Authentication/{audience}").Result;

            context = new ClientAuthenticationContext(token, UserSerializer.Deserialize(token, audience));

            return true;
        }
        catch
        {
            context = default!;
            return false;
        }
    }

    /// <summary>
    /// IAuthenticationContextのJWTによる実装。
    /// </summary>
    private class ClientAuthenticationContext : IClientAuthenticationContext
    {
        public ClientAuthenticationContext(string currentTokenString, User currentUser)
        {
            CurrentTokenString = currentTokenString;
            CurrentUser = currentUser;
        }

        /// <summary>
        /// 認証済みトークンを取得する。
        /// </summary>
        public string CurrentTokenString { get; }

        /// <summary>
        /// 認証済ユーザーを取得する。
        /// </summary>
        public User CurrentUser { get; }
    }

}