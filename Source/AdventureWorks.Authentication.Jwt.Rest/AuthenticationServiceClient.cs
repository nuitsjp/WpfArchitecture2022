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

    public bool TryAuthenticate(AuthenticationContext context, string audience)
    {
        try
        {
            var endpoint = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.Endpoint",
                "https://localhost:4001");
            var token = HttpClient.GetStringAsync($"{endpoint}/Authentication/{audience}").Result;

            context.CurrentUser = UserSerializer.Deserialize(token, audience);
            context.CurrentTokenString = token;

            return true;
        }
        catch
        {
            return false;
        }
    }
}