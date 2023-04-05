using AdventureWorks.Hosting.MagicOnion;

namespace AdventureWorks.Business.MagicOnion;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}
