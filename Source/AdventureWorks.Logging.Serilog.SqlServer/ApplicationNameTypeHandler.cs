using System.Data;
using Dapper;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class ApplicationNameTypeHandler : SqlMapper.TypeHandler<ApplicationName>
{
    public override void SetValue(IDbDataParameter parameter, ApplicationName value)
    {
        parameter.Value = value.Value;
    }

    public override ApplicationName Parse(object value)
    {
        return new(value.ToString()!);
    }
}