using AdventureWorks.AspNetCore.WebApi;

var builder = WebApiApplicationBuilder.CreateBuilder(args);
AdventureWorks.SqlServer.Initializer.Initialize(builder);

builder
    .Build("AdventureWorks.Authentication.Jwt.AspNetCore")
    .Run();