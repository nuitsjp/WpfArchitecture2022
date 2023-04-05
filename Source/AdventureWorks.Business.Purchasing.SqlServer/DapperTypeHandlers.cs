// ReSharper disable RedundantNameQualifier
using System.Data;
using Dapper;

namespace AdventureWorks.Business.Purchasing.SqlServer;

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
        SqlMapper.AddTypeHandler(new VendorIdTypeHandler());
        SqlMapper.AddTypeHandler(new ShipMethodIdTypeHandler());
        SqlMapper.AddTypeHandler(new ProductIdTypeHandler());
        SqlMapper.AddTypeHandler(new ProductCategoryIdTypeHandler());
        SqlMapper.AddTypeHandler(new ProductSubcategoryIdTypeHandler());
        SqlMapper.AddTypeHandler(new UnitMeasureCodeTypeHandler());
    }
}

