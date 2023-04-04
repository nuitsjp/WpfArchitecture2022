using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Hosting.Rest;

var builder = RestApplicationBuilder.CreateBuilder(args);
AdventureWorks.SqlServer.Initializer.Initialize(builder);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);


builder
    .Build("AdventureWorks.Authentication.Hosting.WebApi")
    .Run();