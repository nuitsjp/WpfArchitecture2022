using AdventureWorks.Authentication;

namespace AdventureWorks.Purchasing.App.Driver.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    public string CurrentTokenString { get; } = string.Empty;

    public Employee CurrentEmployee { get; } =
        new(
            new EmployeeId(1),
            new LoginId("LoginId"));

    public async Task<bool> TryAuthenticateAsync(string audience)
    {
        await Task.CompletedTask;
        return true;
    }
}