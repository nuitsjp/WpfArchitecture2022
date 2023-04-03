﻿using Grpc.Core;
using MagicOnion.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.AspNetCore.MagicOnion;

public class AuthenticationAttribute : MagicOnionFilterAttribute
{
    private readonly ILogger<AuthenticationAttribute> _logger;

    public AuthenticationAttribute(ILogger<AuthenticationAttribute> logger)
    {
        _logger = logger;
    }

    public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
    {
        string user;
        try
        {
            var entry = context.CallContext.RequestHeaders.Get("authorization");
            var value = entry.Value;
            //user = CryptoService.Decrypt(value);
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

public interface IServerAuthenticationContext
{

}