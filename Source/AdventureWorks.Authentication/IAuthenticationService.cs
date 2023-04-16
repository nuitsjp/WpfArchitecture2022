namespace AdventureWorks.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticateResult> TryAuthenticateAsync();
}