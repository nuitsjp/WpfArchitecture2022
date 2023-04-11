using MagicOnion.Client;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Client;

public class AuthenticationFilter : IClientFilter
{
    private readonly IMagicOnionClientAuthenticationContext _authenticationContext;
    private readonly string _audience;

    public AuthenticationFilter(
        IMagicOnionClientAuthenticationContext authenticationContext,
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