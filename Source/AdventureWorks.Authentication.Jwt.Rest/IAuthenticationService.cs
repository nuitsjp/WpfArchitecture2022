namespace AdventureWorks.Authentication.Jwt.Rest;

public interface IAuthenticationService
{
    Task<string> AuthenticateAsync(string audience);
}