namespace AdventureWorks.Authentication.Jwt.Rest;

public interface IAuthenticationService
{
    public const string ServiceName = "Authenticate";

    Task<string> AuthenticateAsync();
}