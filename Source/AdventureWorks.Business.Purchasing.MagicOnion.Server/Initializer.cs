using AdventureWorks.Authentication.Jwt;
using AdventureWorks.Hosting.MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

public static class Initializer
{
    public static void Initialize(IMagicOnionServerApplicationBuilder builder)
    {
        builder.AddFormatterResolver(Business.MagicOnion.CustomResolver.Instance);
        builder.AddFormatterResolver(CustomResolver.Instance);
        builder.AddServiceAssembly(typeof(Initializer).Assembly);
        builder.Services.AddSingleton(new Audience("AdventureWorks"));
    }
}