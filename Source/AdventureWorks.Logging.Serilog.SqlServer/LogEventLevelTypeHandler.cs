using System.Data;
using Dapper;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// LogEventLevelのTypeHandler
/// </summary>
public class LogEventLevelTypeHandler : SqlMapper.TypeHandler<LogEventLevel>
{
    /// <summary>
    /// 値を設定する。
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    public override void SetValue(IDbDataParameter parameter, LogEventLevel value)
    {
        parameter.Value = value.ToString();
    }

    /// <summary>
    /// 値を取得する。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override LogEventLevel Parse(object value)
    {
        return (LogEventLevel)Enum.Parse(typeof(LogEventLevel), value.ToString()!);
    }
}