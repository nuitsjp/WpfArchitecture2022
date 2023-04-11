using MagicOnion.Client;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Client;

public class AuthenticationFilter : IClientFilter
{
    private readonly IClientAuthenticationContext _authenticationContext;
    private readonly string _audience;

    public AuthenticationFilter(
        IClientAuthenticationContext authenticationContext,
        string audience)
    {
        _authenticationContext = authenticationContext;
        _audience = audience;
    }

    public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
    {
        var header = context.CallOptions.Headers;
        header.Add("authorization", $"Bearer {_authenticationContext.CurrentTokenString}");
        header.Add("audience", _audience);

        return await next(context);
    }
}