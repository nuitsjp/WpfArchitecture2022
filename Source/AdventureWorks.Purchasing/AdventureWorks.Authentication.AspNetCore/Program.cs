using AdventureWorks.AspNetCore.WebApi;

WebApiServerBuilder
    .CreateBuilder(args)
    .Build("AdventureWorks.Authentication.AspNetCore")
    .Run();
