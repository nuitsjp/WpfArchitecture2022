using MessagePack;
using MessagePack.Resolvers;

namespace AdventureWorks.Hosting.MagicOnion;

public class FormatterResolverCollection
{
    private readonly List<IFormatterResolver> _resolvers = new();

    public void Add(IFormatterResolver resolver)
    {
        if (_resolvers.Contains(resolver))
        {
            return;
        }

        _resolvers.Add(resolver);
    }

    public void InitializeResolver()
    {
        _resolvers.Insert(0, StandardResolver.Instance);
        _resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(_resolvers.ToArray());
        MessagePackSerializer.DefaultOptions =
            ContractlessStandardResolver.Options
                .WithResolver(StaticCompositeResolver.Instance);
    }
}