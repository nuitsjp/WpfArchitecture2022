using MessagePack;
using MessagePack.Resolvers;

namespace AdventureWorks.MagicOnion;

public class MessagePackInitializer
{
    private readonly List<IFormatterResolver> _resolvers = new();

    public void Add(IFormatterResolver resolver) => _resolvers.Add(resolver);

    public void Initialize()
    {
        _resolvers.Insert(0, StandardResolver.Instance);
        _resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
        MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
            .WithResolver(StaticCompositeResolver.Instance);

    }
}