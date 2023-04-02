using AdventureWorks.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder)
    {
        builder.Services.AddSingleton(_ => builder.Configuration.GetRequiredSection("MagicOnion").Get<MagicOnionConfig>()!);
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}