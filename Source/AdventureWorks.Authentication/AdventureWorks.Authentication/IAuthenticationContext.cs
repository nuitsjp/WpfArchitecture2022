namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    Employee CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync();
}