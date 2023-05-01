
using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;
using AdventureWorks.Logging.Serilog.MagicOnion.Server;
using AdventureWorks.Logging.Serilog.SqlServer;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

builder.UseSerilogMagicOnionServer();
builder.UseSerilogSqlServer();

var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();