using AdventureWorks.Authentication;
using MagicOnion;

namespace AdventureWorks.Business.MagicOnion;

public interface IMagicOnionClientFactory
{
    T Create<T>(IAuthenticationContext authenticationContext) where T : IService<T>;
}