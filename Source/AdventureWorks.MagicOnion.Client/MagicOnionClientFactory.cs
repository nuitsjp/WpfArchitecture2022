using AdventureWorks.Authentication;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.MagicOnion.Client;

public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    private readonly IAuthenticationContext _authenticationContext;
    private readonly string _endpoint;
    private readonly string _audience;

    public MagicOnionClientFactory(
        IAuthenticationContext authenticationContext, 
        string endpoint, 
        string audience)
    {
        _authenticationContext = authenticationContext;
        _endpoint = endpoint;
        _audience = audience;
    }

    public T Create<T>() where T : IService<T>
    {
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(_endpoint),
            new IClientFilter[]
            {
                new AuthenticationFilter(_authenticationContext, _audience)
            });
    }

    public class AuthenticationFilter : IClientFilter
    {
        private readonly IAuthenticationContext _authenticationContext;
        private readonly string _audience;

        public AuthenticationFilter(
            IAuthenticationContext authenticationContext, 
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
}