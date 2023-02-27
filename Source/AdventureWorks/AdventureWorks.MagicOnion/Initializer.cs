using AdventureWorks.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddSingleton(_ => builder.Configuration.GetRequiredSection("MagicOnion").Get<MagicOnionConfig>()!);
        builder.Add(CustomResolver.Instance);
    }
}