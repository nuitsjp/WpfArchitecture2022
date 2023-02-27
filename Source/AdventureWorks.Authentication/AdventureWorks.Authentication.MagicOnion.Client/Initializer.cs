using AdventureWorks.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.MagicOnion.Client;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder) =>
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
}