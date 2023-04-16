namespace AdventureWorks.Authentication.Jwt.Rest.Client;

public class AuthenticationService : IAuthenticationService
{
    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    private readonly ClientAuthenticationContext _context;

    private readonly Audience _audience;

    public AuthenticationService(
        ClientAuthenticationContext context, 
        Audience audience)
    {
        _context = context;
        _audience = audience;
    }

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
            return new(false, _context);
        }
    }
}