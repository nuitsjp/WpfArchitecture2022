using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest.Client;

public class ClientAuthenticationContext : IAuthenticationContext
{
    public User CurrentUser { get; internal set; } = default!;
    public string CurrentTokenString { get; internal set; } = default!;
}