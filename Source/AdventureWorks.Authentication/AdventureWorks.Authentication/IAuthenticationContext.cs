namespace AdventureWorks.Authentication;

public interface IAuthenticationContext
{
    string CurrentTokenString { get; }
    Employee CurrentEmployee { get; }
    Task<bool> TryAuthenticateAsync();
}

public record User(EmployeeId EmployeeId, string Name);
