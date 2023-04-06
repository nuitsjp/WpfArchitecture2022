using AdventureWorks.Authentication;
using AdventureWorks.Business;

namespace AdventureWorks.Purchasing.App.Driver.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    public string CurrentTokenString { get; } = string.Empty;

    public User CurrentEmployee { get; } =
        new(
            new EmployeeId(1),
            "Adventure Works");

    public async Task<bool> TryAuthenticateAsync(string audience)
    {
        await Task.CompletedTask;
        return true;
    }
}