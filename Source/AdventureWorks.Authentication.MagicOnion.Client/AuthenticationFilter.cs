using MagicOnion.Client;

namespace AdventureWorks.Authentication.MagicOnion.Client;

public class AuthenticationFilter : IClientFilter
{
    private readonly IAuthenticationContext _authenticationContext;

    public AuthenticationFilter(
        IAuthenticationContext authenticationContext)
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