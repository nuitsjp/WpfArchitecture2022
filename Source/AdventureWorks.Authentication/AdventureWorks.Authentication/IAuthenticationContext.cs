namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    string CurrentTokenString { get; }
    Employee CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync(string audience);
}

public record User(EmployeeId EmployeeId, string Name);
