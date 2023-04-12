using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// 認証処理RESTサービスを呼び出すためのクライアント
/// </summary>
public class AuthenticationServiceClient
{

    public bool TryAuthenticate(out IClientAuthenticationContext context)
    {
        try
        {
            var baseAddress = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.BaseAddress",
                "https://localhost:4001");
            context = AuthenticateAsync(baseAddress).Result;

            return true;
        }
        catch
        {
            context = default!;
            return false;
        }
    }

    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    private async Task<ClientAuthenticationContext> AuthenticateAsync(string baseAddress)
    {
        var token = await HttpClient.GetStringAsync($"{baseAddress}/Authentication");
        return new ClientAuthenticationContext(token, UserSerializer.Deserialize(token));
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