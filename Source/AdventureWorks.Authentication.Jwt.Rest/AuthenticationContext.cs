using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest;

public class AuthenticationContext : IAuthenticationContext
{
    private string? _tokenString;
    private User? _currentEmployee;

    private static readonly HttpClient HttpClient = new(new HttpClientHandler {UseDefaultCredentials = true});

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

    public User CurrentEmployee
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

    public async Task<bool> TryAuthenticateAsync(string audience)
    {
        var endpoint = Environments.GetEnvironmentVariable(
            "AdventureWorks.Authentication.Jwt.Rest.Endpoint",
            "https://localhost:4001");
        var response = await HttpClient.GetAsync($"{endpoint}/Authentication");
        response.EnsureSuccessStatusCode();

        _tokenString = await response.Content.ReadAsStringAsync();
        _currentEmployee = UserSerializer.Deserialize(_tokenString, "AdventureWorks.Authentication");


        return true;
    }
}
