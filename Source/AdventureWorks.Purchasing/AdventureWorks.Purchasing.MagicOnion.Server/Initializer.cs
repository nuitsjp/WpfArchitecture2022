using AdventureWorks.AspNetCore;
using AdventureWorks.MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(ApplicationBuilder builder, MessagePackInitializer messagePackInitializer)
    {
        MagicOnion.Initializer.Initialize(builder, messagePackInitializer);
        builder.Add(typeof(Initializer).Assembly);
    }
}