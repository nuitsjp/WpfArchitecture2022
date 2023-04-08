using AdventureWorks.Hosting.Rest;
using AdventureWorks.Logging.Serilog.Rest.Server;

var builder = RestApplicationBuilder.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(SerilogController).Assembly);


var app = await builder.BuildAsync("AdventureWorks.Logging.Hosting.Rest");
app.Run();