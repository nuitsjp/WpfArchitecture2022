namespace AdventureWorks.Authentication;

public interface IAuthenticationService
{
    Task<bool> TryAuthenticateAsync();
}