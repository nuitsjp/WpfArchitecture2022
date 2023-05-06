using AdventureWorks.Hosting;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// 初期化拡張メソッドクラス
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// SerilogのSqlServerを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    public static void UseSerilogSqlServer(this IApplicationBuilder builder)
    {
        SqlMapper.AddTypeHandler(new LogEventLevelTypeHandler());
        SqlMapper.AddTypeHandler(new ApplicationNameTypeHandler());

        builder.Services.AddTransient<SerilogDatabase>();
        builder.Services.AddTransient<ISerilogConfigRepository, SerilogConfigRepository>();
        builder.Services.AddTransient<ILogRepository, LogRepository>();
    }
}