// ReSharper disable RedundantNameQualifier
using System.Data;
using Dapper;

namespace AdventureWorks.Authentication.Service.Database;

public class EmployeeIdTypeHandler : SqlMapper.TypeHandler<EmployeeId>
{
    public override void SetValue(IDbDataParameter parameter, EmployeeId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override EmployeeId Parse(object value)
    {
        return new EmployeeId((System.Int32)value);
    }
}

public class LoginIdTypeHandler : SqlMapper.TypeHandler<LoginId>
{
    public override void SetValue(IDbDataParameter parameter, LoginId value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.AsPrimitive();
    }

    public override LoginId Parse(object value)
    {
        return new LoginId((System.String)value);
    }
}


public static class TypeHandlerInitializer
{
    public static void Initialize()
    {
        SqlMapper.AddTypeHandler(new EmployeeIdTypeHandler());
        SqlMapper.AddTypeHandler(new LoginIdTypeHandler());
    }
}

