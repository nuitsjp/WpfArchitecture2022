using AdventureWorks.Hosting.Rest;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.Rest.Server;
using AdventureWorks.Logging.Serilog.SqlServer;

var builder = RestApplicationBuilder.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(SerilogConfigController).Assembly);

builder.Services.AddTransient<SerilogDatabase>();
builder.Services.AddTransient<ISerilogConfigRepository, SerilogConfigRepository>();


var app = await builder.BuildAsync("AdventureWorks.Logging.Hosting.Rest");
app.Run();