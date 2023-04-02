using AdventureWorks.AspNetCore.WebApi;

WebApiApplicationBuilder
    .CreateBuilder(args)
    .Build("AdventureWorks.Authentication.AspNetCore")
    .Run();
