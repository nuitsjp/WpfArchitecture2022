using AdventureWorks.Business;

namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    string CurrentTokenString { get; }
    User CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync(string audience);
}

