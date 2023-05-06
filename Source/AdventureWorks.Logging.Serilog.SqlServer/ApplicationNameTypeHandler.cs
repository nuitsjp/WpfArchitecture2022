using System.Data;
using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

/// <summary>
/// ApplicationNameのTypeHandler
/// </summary>
public class ApplicationNameTypeHandler : SqlMapper.TypeHandler<ApplicationName>
{
    /// <summary>
    /// 値を設定する。
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="value"></param>
    public override void SetValue(IDbDataParameter parameter, ApplicationName value)
    {
        parameter.Value = value.Value;
    }

    /// <summary>
    /// 値を取得する。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override ApplicationName Parse(object value)
    {
        return new(value.ToString()!);
    }
}