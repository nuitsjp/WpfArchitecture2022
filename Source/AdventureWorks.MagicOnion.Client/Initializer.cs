using AdventureWorks.Hosting.MagicOnion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder)
    {
        builder.Services.AddTransient<IMagicOnionClientFactory, MagicOnionClientFactory>();
    }
}
