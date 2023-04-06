using MagicOnion;

namespace AdventureWorks.MagicOnion.Client;

public interface IMagicOnionClientFactory
{
    T Create<T>() where T : IService<T>;
}