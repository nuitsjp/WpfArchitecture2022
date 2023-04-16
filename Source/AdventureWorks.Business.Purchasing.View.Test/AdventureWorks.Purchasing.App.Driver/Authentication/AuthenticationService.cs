using AdventureWorks.Authentication;

namespace AdventureWorks.Purchasing.App.Driver.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public async Task<AuthenticateResult> TryAuthenticateAsync()
    {
        await Task.CompletedTask;
        return new AuthenticateResult(true, new AuthenticationContext());
    }
}