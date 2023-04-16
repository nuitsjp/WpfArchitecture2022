using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Hosting.AspNetCore;
using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Hosting.MagicOnion.Server;

public class AuthenticationFilterAttribute : MagicOnionFilterAttribute
{
    private readonly ILogger<AuthenticationFilterAttribute> _logger;

    private readonly ServerAuthenticationContext _serverAuthenticationContext;
    private readonly Audience _audience;

    public AuthenticationFilterAttribute(
        ILogger<AuthenticationFilterAttribute> logger, 
        IAuthenticationContext authenticationContext, 
        Audience audience)
    {
        _logger = logger;
        _audience = audience;
        _serverAuthenticationContext = (ServerAuthenticationContext)authenticationContext;
    }

    public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        try
        {
            var entry = context.CallContext.RequestHeaders.Get("authorization");
            var token = entry.Value.Substring("Bearer ".Length);
            _serverAuthenticationContext.CurrentUser = UserSerializer.Deserialize(token, _audience);
            _serverAuthenticationContext.CurrentTokenString = token;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.CallContext.GetHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        try
        {
            await next(context);
        }
        finally
        {
            _serverAuthenticationContext.ClearCurrentUser();
        }
    }
}