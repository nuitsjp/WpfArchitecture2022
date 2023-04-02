using AdventureWorks.Authentication;

namespace AdventureWorks.Purchasing.App.Driver.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    public string CurrentToken { get; } = string.Empty;

    public Employee CurrentEmployee { get; } =
        new(
            new EmployeeId(1),
            new LoginId("LoginId"));

    public async Task<bool> TryAuthenticateAsync()
    {
        await Task.CompletedTask;
        return true;
    }
}