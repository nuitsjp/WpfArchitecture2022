using System.Data;
using Dapper;
using Serilog.Events;

namespace AdventureWorks.Logging.Serilog.SqlServer;

public class LogEventLevelTypeHandler : SqlMapper.TypeHandler<LogEventLevel>
{
    public override void SetValue(IDbDataParameter parameter, LogEventLevel value)
    {
        parameter.Value = value.ToString();
    }

    public override LogEventLevel Parse(object value)
    {
        throw new NotImplementedException();
    }
}