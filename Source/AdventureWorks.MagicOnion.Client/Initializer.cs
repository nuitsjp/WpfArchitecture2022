using AdventureWorks.Authentication;
using AdventureWorks.Hosting.MagicOnion;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder, string audience)
    {
        builder.Services.AddTransient<IMagicOnionClientFactory>(
            provider =>
            {
                var baseAddress = Environments.GetEnvironmentVariable(
                    "AdventureWorks.Business.Purchasing.MagicOnion.BaseAddress",
                    "https://localhost:5001");

                return new MagicOnionClientFactory(
                    provider.GetRequiredService<IAuthenticationContext>(),
                    baseAddress);
            });
    }
}
