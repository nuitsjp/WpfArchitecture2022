// ReSharper disable RedundantNameQualifier
using System.Data;
using Dapper;

namespace AdventureWorks.Purchasing.Database;

public class VendorIdTypeHandler : SqlMapper.TypeHandler<VendorId>
{
    public override void SetValue(IDbDataParameter parameter, VendorId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override VendorId Parse(object value)
    {
        return new VendorId((System.Int32)value);
    }
}

public class ShipMethodIdTypeHandler : SqlMapper.TypeHandler<ShipMethodId>
{
    public override void SetValue(IDbDataParameter parameter, ShipMethodId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override ShipMethodId Parse(object value)
    {
        return new ShipMethodId((System.Int32)value);
    }
}


public static class TypeHandlerInitializer
{
    public static void Initialize()
    {
        SqlMapper.AddTypeHandler(new VendorIdTypeHandler());
        SqlMapper.AddTypeHandler(new ShipMethodIdTypeHandler());
    }
}

