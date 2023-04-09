using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Server;

public class AuthenticationAttribute : MagicOnionFilterAttribute
{
    private readonly ILogger<AuthenticationAttribute> _logger;

    public AuthenticationAttribute(ILogger<AuthenticationAttribute> logger)
    {
        _logger = logger;
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
            var audience = context.CallContext.RequestHeaders.Get("audience").Value;
            var user = UserSerializer.Deserialize(token, audience);
            _logger.LogInformation($"{context.CallContext.Method} Peer:{context.CallContext.Peer} EmployeeId:{user.EmployeeId} Name:{user.Name}");
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, e.Message);
            context.CallContext.GetHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context); // next
    }
}
