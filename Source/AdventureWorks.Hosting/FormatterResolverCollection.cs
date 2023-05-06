using MessagePack;
using MessagePack.Resolvers;

namespace AdventureWorks.Hosting;

/// <summary>
/// IFormatterResolver集合
/// </summary>
public class FormatterResolverCollection
{
    /// <summary>
    /// IFormatterResolver集合の実体
    /// </summary>
    private readonly List<IFormatterResolver> _resolvers = new();

    /// <summary>
    /// IFormatterResolverを追加する
    /// </summary>
    /// <param name="resolver"></param>
    public void Add(IFormatterResolver resolver)
    {
        if (_resolvers.Contains(resolver))
        {
            return;
        }

        _resolvers.Add(resolver);
    }

    /// <summary>
    /// IFormatterResolverを初期化する。
    /// </summary>
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