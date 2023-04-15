using AdventureWorks.Business;
using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Authentication.Jwt.Rest.Client;

/// <summary>
/// 認証処理RESTサービスを呼び出すためのクライアント
/// </summary>
public static class AuthenticationServiceClient
{
    public static async Task<IClientAuthenticationContext> AuthenticateAsync()
    {
        var baseAddress = Environments.GetEnvironmentVariable(
            "AdventureWorks.Authentication.Jwt.Rest.BaseAddress",
            "https://localhost:4001");
        var token = await HttpClient.GetStringAsync($"{baseAddress}/Authentication");
        var context = new ClientAuthenticationContext(token, UserSerializer.Deserialize(token));
        return context;
    }

    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

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