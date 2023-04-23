using AdventureWorks.Hosting;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public static class Initializer
{
    internal static void InitializeTypeHandler()
    {
        SqlMapper.AddTypeHandler(new LogEventLevelTypeHandler());
        SqlMapper.AddTypeHandler(new ApplicationNameTypeHandler());
    }

    public static void Initialize(IApplicationBuilder builder)
    {
        InitializeTypeHandler();

        builder.Services.AddTransient<SerilogDatabase>();
        builder.Services.AddTransient<ISerilogConfigRepository, SerilogConfigRepository>();
        builder.Services.AddTransient<ILogRepository, LogRepository>();
    }
}