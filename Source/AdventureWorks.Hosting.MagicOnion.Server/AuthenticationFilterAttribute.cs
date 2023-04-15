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
            const string bearer = "Bearer ";
            var entry = context.CallContext.RequestHeaders.Get("authorization");
            var value = entry.Value;
            if (value.StartsWith(bearer) is false)
            {
                context.CallContext.GetHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var token = value.Substring(bearer.Length);
            var user = UserSerializer.Deserialize(token, _audience);
            _serverAuthenticationContext.CurrentUser = user;
            _serverAuthenticationContext.CurrentTokenString = token;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, e.Message);
            context.CallContext.GetHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        try
        {
            await next(context); // next
        }
        finally
        {
            _serverAuthenticationContext.ClearCurrentUser();
        }
    }
}