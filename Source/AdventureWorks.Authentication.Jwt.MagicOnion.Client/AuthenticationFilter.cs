using MagicOnion.Client;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Client;

public class AuthenticationFilter : IClientFilter
{
    private readonly IClientAuthenticationContext _authenticationContext;

    public AuthenticationFilter(
        IClientAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        var header = context.CallOptions.Headers;
        header.Add("authorization", $"Bearer {_authenticationContext.CurrentTokenString}");

        return await next(context);
    }
}