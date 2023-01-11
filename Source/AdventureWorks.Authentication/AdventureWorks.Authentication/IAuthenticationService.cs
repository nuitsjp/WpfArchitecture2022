namespace AdventureWorks.Authentication;

public interface IAuthenticationService
{
    Employee CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync();
}