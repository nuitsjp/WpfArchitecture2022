using AdventureWorks.Hosting;

namespace AdventureWorks.Business.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}
