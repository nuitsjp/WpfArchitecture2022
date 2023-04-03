﻿using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.Jwt;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IAuthenticationContext, AuthenticationContext>();
    }
}