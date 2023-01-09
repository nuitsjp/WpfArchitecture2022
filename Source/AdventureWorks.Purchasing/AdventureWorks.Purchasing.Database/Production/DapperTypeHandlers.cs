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

public class ProductCategoryIdTypeHandler : SqlMapper.TypeHandler<ProductCategoryId>
{
    public override void SetValue(IDbDataParameter parameter, ProductCategoryId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override ProductCategoryId Parse(object value)
    {
        return new ProductCategoryId((System.Int32)value);
    }
}

public class ProductSubcategoryIdTypeHandler : SqlMapper.TypeHandler<ProductSubcategoryId>
{
    public override void SetValue(IDbDataParameter parameter, ProductSubcategoryId value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override ProductSubcategoryId Parse(object value)
    {
        return new ProductSubcategoryId((System.Int32)value);
    }
}

public class UnitMeasureCodeTypeHandler : SqlMapper.TypeHandler<UnitMeasureCode>
{
    public override void SetValue(IDbDataParameter parameter, UnitMeasureCode value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.AsPrimitive();
    }

    public override UnitMeasureCode Parse(object value)
    {
        return new UnitMeasureCode((System.String)value);
    }
}


public static class TypeHandlerInitializer
{
    public static void Initialize()
    {
        SqlMapper.AddTypeHandler(new ProductIdTypeHandler());
        SqlMapper.AddTypeHandler(new ProductCategoryIdTypeHandler());
        SqlMapper.AddTypeHandler(new ProductSubcategoryIdTypeHandler());
        SqlMapper.AddTypeHandler(new UnitMeasureCodeTypeHandler());
    }
}

