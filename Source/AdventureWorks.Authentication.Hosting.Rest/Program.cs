using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Business.SqlServer;
using AdventureWorks.Hosting.Rest;

var builder = RestApplicationBuilder.CreateBuilder(args);
Initializer.Initialize(builder);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);


var app = await builder.BuildAsync("AdventureWorks.Authentication.Hosting.WebApi");
await app.RunAsync();