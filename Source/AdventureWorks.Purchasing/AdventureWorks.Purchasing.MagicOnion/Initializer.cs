using AdventureWorks.Extensions;
using AdventureWorks.MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder, MessagePackInitializer messagePackInitializer)
    {
        messagePackInitializer.Add(CustomResolver.Instance);
        messagePackInitializer.Add(Production.CustomResolver.Instance);
    }
}