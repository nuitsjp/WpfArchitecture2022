using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.Jwt.Rest;

public static class Initializer
{
    public static IAuthenticationContext Initialize(IApplicationBuilder builder)
    {
        AuthenticationContext context = new();
        builder.Services.AddSingleton<IAuthenticationContext>(context);
        return context;
    }
}