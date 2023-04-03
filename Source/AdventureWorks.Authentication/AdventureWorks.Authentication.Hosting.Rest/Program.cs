using AdventureWorks.AspNetCore.WebApi;
using AdventureWorks.Authentication.Jwt.Rest.Server;

var builder = WebApiApplicationBuilder.CreateBuilder(args);
AdventureWorks.SqlServer.Initializer.Initialize(builder);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);


builder
    .Build("AdventureWorks.Authentication.Hosting.WebApi")
    .Run();