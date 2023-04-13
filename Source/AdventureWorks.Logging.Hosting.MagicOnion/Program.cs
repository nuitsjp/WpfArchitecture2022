
using AdventureWorks.Hosting.MagicOnion.Server;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

AdventureWorks.Logging.Serilog.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Logging.Serilog.SqlServer.Initializer.Initialize(builder);

var app = await builder.BuildAsync(typeof(Program).Assembly.GetName().Name!);
app.Run();