using AdventureWorks.Authentication;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.Business.MagicOnion;

public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    private readonly MagicOnionConfig _config;

    public MagicOnionClientFactory(MagicOnionConfig config)
    {
        _config = config;
    }

    public T Create<T>(IAuthenticationContext authenticationContext) where T : IService<T>
    {
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(_config.Address),
            new IClientFilter[]
            {
                new AuthenticationFilter(authenticationContext)
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