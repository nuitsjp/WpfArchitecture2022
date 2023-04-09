using AdventureWorks.Authentication;
using AdventureWorks.Hosting.MagicOnion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder, string audience)
    {
        builder.Services.AddTransient<IMagicOnionClientFactory>(
            provider =>
            {
                var endpoint = Environments.GetEnvironmentVariable(
                    "AdventureWorks.Business.Purchasing.MagicOnion.Endpoint",
                    "https://localhost:5001");

                return new MagicOnionClientFactory(
                    provider.GetRequiredService<IAuthenticationContext>(),
                    endpoint,
                    audience);
            });
    }
}
