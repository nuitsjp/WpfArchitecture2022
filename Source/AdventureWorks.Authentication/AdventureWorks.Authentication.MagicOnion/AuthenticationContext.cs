using System.Net;
using AdventureWorks.MagicOnion;
using System.Security.Authentication;
using AdventureWorks.Authentication.Jwt;

namespace AdventureWorks.Authentication.MagicOnion;

public class AuthenticationContext : IAuthenticationContext
{
    private static readonly HttpClient AuthenticationClient = new(new HttpClientHandler {UseDefaultCredentials = true});
    private readonly MagicOnionConfig _config;
    private readonly IMagicOnionClientFactory _clientFactory;
    private string? _tokenString;
    private Employee? _currentEmployee;


    public AuthenticationContext(
        MagicOnionConfig config, 
        IMagicOnionClientFactory clientFactory)
    {
        _config = config;
        _clientFactory = clientFactory;
    }

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

    public Employee CurrentEmployee
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

    public async Task<bool> TryAuthenticateAsync()
    {
        var response = await AuthenticationClient.GetAsync("https://localhost:4001/Authentication");
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new AuthenticationException();
        }

        _tokenString = await response.Content.ReadAsStringAsync();
        _currentEmployee = EmployeeSerializer.Deserialize(_tokenString, "");

        return _currentEmployee is not null;
    }
}