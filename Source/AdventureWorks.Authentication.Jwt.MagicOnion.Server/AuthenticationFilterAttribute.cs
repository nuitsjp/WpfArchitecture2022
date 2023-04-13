using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Server;

public class AuthenticationFilterAttribute : MagicOnionFilterAttribute
{
    private readonly ILogger<AuthenticationFilterAttribute> _logger;

    private readonly ServerAuthenticationContext _serverAuthenticationContext;

    public AuthenticationFilterAttribute(
        ILogger<AuthenticationFilterAttribute> logger, 
        IAuthenticationContext authenticationContext)
    {
        _logger = logger;
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
            var user = UserSerializer.Deserialize(token);
            _serverAuthenticationContext.CurrentUser = user;
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