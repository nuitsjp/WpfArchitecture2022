using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Hosting.Rest;
using AdventureWorks.Logging.Serilog;

var builder = RestApplicationBuilder.CreateBuilder(args);
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);


var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
await app.RunAsync();