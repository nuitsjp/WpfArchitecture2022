namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    string CurrentToken { get; }
    Employee CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync();
}

public record AuthenticationInfo(string Token, string EmployeeId);