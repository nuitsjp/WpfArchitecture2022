using AdventureWorks.Business;

namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    string CurrentTokenString { get; }
    User CurrentEmployee { get; }
    bool TryAuthenticate(string audience);
}

