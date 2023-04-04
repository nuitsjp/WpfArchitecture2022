namespace AdventureWorks.Authentication.Jwt.Rest;

public class AuthenticationContext : IAuthenticationContext
{
    private string? _tokenString;
    private Employee? _currentEmployee;

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

    public async Task<bool> TryAuthenticateAsync(string audience)
    {
        var response = await HttpClient.GetAsync($"https://localhost:4001/Authentication");
        response.EnsureSuccessStatusCode();

        _tokenString = await response.Content.ReadAsStringAsync();
        _currentEmployee = EmployeeSerializer.Deserialize(_tokenString, "AdventureWorks.Authentication");


        return true;
    }
}
