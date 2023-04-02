using MessagePack;

namespace AdventureWorks.Hosting;

public interface IMagicOnionApplicationBuilder : IApplicationBuilder
{
    void AddFormatterResolver(IFormatterResolver resolver);
}