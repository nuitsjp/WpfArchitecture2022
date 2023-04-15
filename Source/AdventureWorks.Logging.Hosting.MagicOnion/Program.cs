
using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

AdventureWorks.Logging.Serilog.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Logging.Serilog.SqlServer.Initializer.Initialize(builder);

var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();