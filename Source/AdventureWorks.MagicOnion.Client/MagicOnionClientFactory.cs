using AdventureWorks.Authentication;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.MagicOnion.Client;

public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    private readonly IClientAuthenticationContext _authenticationContext;
    private readonly Endpoint _endpoint;

    public MagicOnionClientFactory(
        IClientAuthenticationContext authenticationContext,
        Endpoint endpoint)
    {
        _authenticationContext = authenticationContext;
        _endpoint = endpoint;
    }

    public T Create<T>() where T : IService<T>
    {
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(_endpoint.Uri),
            new IClientFilter[]
            {
                new AuthenticationFilter(_authenticationContext)
            });
    }

}