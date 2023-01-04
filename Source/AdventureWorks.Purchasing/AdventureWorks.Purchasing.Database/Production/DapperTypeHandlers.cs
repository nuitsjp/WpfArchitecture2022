using AdventureWorks.Purchasing.Production;
// ReSharper disable RedundantNameQualifier
using System.Data;
using Dapper;

namespace AdventureWorks.Purchasing.Database.Production;

public class ProductIdTypeHandler : SqlMapper.TypeHandler<ProductId>
{
    public override void SetValue(IDbDataParameter parameter, ProductId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override ProductId Parse(object value)
    {
        return new ProductId((System.Int32)value);
    }
}


public static class TypeHandlerInitializer
{
    public static void Initialize()
    {
        SqlMapper.AddTypeHandler(new ProductIdTypeHandler());
    }
}

