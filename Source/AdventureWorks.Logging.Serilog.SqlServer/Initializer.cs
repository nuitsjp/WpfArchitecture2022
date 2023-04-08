using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient<SerilogDatabase>();
        builder.Services.AddTransient<ISerilogConfigRepository, SerilogConfigRepository>();
    }
}