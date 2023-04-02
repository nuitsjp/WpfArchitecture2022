using AdventureWorks.Hosting;
using AdventureWorks.MagicOnion;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.MagicOnion;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
        builder.Services.AddTransient<IMagicOnionClientFactory, MagicOnionClientFactory>();
    }
}