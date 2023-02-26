using AdventureWorks.AspNetCore;
using AdventureWorks.Extensions;
using AdventureWorks.MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Add(CustomResolver.Instance);
        builder.Add(Production.CustomResolver.Instance);
    }
}