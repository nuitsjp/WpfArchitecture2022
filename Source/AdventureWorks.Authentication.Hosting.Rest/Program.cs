using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Hosting.Rest;

var builder = RestApplicationBuilder.CreateBuilder(args);
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);


var app = await builder.BuildAsync("AdventureWorks.Authentication");
await app.RunAsync();