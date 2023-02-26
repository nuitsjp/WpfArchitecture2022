using AdventureWorks.Extensions;
using AdventureWorks.MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder, MessagePackInitializer messagePackInitializer)
    {
        MagicOnion.Initializer.Initialize(builder, messagePackInitializer);
    }
}