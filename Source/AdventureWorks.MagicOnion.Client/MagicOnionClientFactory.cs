using AdventureWorks.Authentication;
using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace AdventureWorks.MagicOnion.Client;

public class MagicOnionClientFactory : IMagicOnionClientFactory
{
    private readonly IClientAuthenticationContext _authenticationContext;
    private readonly string _endpoint;

    public MagicOnionClientFactory(
        IAuthenticationContext authenticationContext, 
        string endpoint)
    {
        _authenticationContext = (IClientAuthenticationContext)authenticationContext;
        _endpoint = endpoint;
    }

    public T Create<T>() where T : IService<T>
    {
        return MagicOnionClient.Create<T>(
            GrpcChannel.ForAddress(_endpoint),
            new IClientFilter[]
            {
                new AuthenticationFilter(_authenticationContext)
            });
    }

}