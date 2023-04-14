using AdventureWorks.Authentication;
using AdventureWorks.Hosting.MagicOnion;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IMagicOnionApplicationBuilder builder, List<IFormatterResolver> resolvers)
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
        resolvers.Insert(0, StandardResolver.Instance);
        resolvers.Add(ContractlessStandardResolver.Instance);
        StaticCompositeResolver.Instance.Register(resolvers.ToArray());
        MessagePackSerializer.DefaultOptions = ContractlessStandardResolver.Options
            .WithResolver(StaticCompositeResolver.Instance);
    }
}
