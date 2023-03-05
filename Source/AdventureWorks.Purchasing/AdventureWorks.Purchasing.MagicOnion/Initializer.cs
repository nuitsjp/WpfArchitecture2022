using AdventureWorks.Hosting;

namespace AdventureWorks.Purchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Add(CustomResolver.Instance);
        builder.Add(Production.CustomResolver.Instance);
    }
}