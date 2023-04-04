using AdventureWorks.Authentication;
using Grpc.Core;
using MagicOnion;

namespace AdventureWorks.MagicOnion;

public interface IMagicOnionClientFactory
{
    T Create<T>(IAuthenticationContext authenticationContext) where T : IService<T>;
}