using MessagePack;

namespace AdventureWorks.Hosting.MagicOnion;

public interface IMagicOnionApplicationBuilder : IApplicationBuilder
{
    void AddFormatterResolver(IFormatterResolver resolver);
}