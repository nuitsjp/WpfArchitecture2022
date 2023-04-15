namespace AdventureWorks.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticateResult> TryAuthenticateAsync();
}

public record AuthenticateResult(bool IsAuthenticated, IAuthenticationContext Context);