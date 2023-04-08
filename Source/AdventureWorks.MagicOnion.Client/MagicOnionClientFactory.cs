﻿using AdventureWorks.Authentication;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.MagicOnion.Client;

public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    private readonly IAuthenticationContext _authenticationContext;

    public MagicOnionClientFactory(IAuthenticationContext authenticationContext)
    {
        _authenticationContext = authenticationContext;
    }

    public T Create<T>() where T : IService<T>
    {
        var endpoint = Environments.GetEnvironmentVariable(
            "AdventureWorks.MagicOnion.Client.Endpoint",
            "https://localhost:5001");
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(endpoint),
            new IClientFilter[]
            {
                new AuthenticationFilter(_authenticationContext)
            });
    }

    public class AuthenticationFilter : IClientFilter
    {
        private readonly IAuthenticationContext _authenticationContext;

        public AuthenticationFilter(IAuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
        }

        public async ValueTask<ResponseContext> SendAsync(RequestContext context, Func<RequestContext, ValueTask<ResponseContext>> next)
        {
            var header = context.CallOptions.Headers;
            header.Add("authorization", _authenticationContext.CurrentTokenString);

            return await next(context);
        }
    }
}