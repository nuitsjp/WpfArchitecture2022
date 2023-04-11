using AdventureWorks.Authentication;
using AdventureWorks.Business;

namespace AdventureWorks.Purchasing.App.Driver.Authentication;

public class AuthenticationContext : IAuthenticationContext
{
    public string CurrentTokenString { get; } = string.Empty;

    public User CurrentUser { get; } = new(new EmployeeId(1));

    public bool TryAuthenticate(string audience)
    {
        return true;
    }
}