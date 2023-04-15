using AdventureWorks.Hosting;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        SqlMapper.AddTypeHandler(new LogEventLevelTypeHandler());

        builder.Services.AddTransient<SerilogDatabase>();
        builder.Services.AddTransient<ISerilogConfigRepository, SerilogConfigRepository>();
        builder.Services.AddTransient<ILogRepository, LogRepository>();
    }
}