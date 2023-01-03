// ReSharper disable RedundantNameQualifier
using System.Data;
using System.Runtime.CompilerServices;
using Dapper;

namespace AdventureWorks.Database;


public class DateTypeHandler : SqlMapper.TypeHandler<Date>
{
    public override void SetValue(IDbDataParameter parameter, Date value)
    {
        parameter.DbType = DbType.DateTime;
        parameter.Value = value.AsPrimitive();
    }

    public override Date Parse(object value)
    {
        return new Date((System.DateTime)value);
    }
}


public class DaysTypeHandler : SqlMapper.TypeHandler<Days>
{
    public override void SetValue(IDbDataParameter parameter, Days value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override Days Parse(object value)
    {
        return new Days((System.Int32)value);
    }
}


public class DollarTypeHandler : SqlMapper.TypeHandler<Dollar>
{
    public override void SetValue(IDbDataParameter parameter, Dollar value)
    {
        parameter.DbType = DbType.Decimal;
        parameter.Value = value.AsPrimitive();
    }

    public override Dollar Parse(object value)
    {
        return new Dollar((System.Decimal)value);
    }
}


public class DollarPerGramTypeHandler : SqlMapper.TypeHandler<DollarPerGram>
{
    public override void SetValue(IDbDataParameter parameter, DollarPerGram value)
    {
        parameter.DbType = DbType.Decimal;
        parameter.Value = value.AsPrimitive();
    }

    public override DollarPerGram Parse(object value)
    {
        return new DollarPerGram((System.Decimal)value);
    }
}


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


public class GramTypeHandler : SqlMapper.TypeHandler<Gram>
{
    public override void SetValue(IDbDataParameter parameter, Gram value)
    {
        parameter.DbType = DbType.Decimal;
        parameter.Value = value.AsPrimitive();
    }

    public override Gram Parse(object value)
    {
        return new Gram((System.Decimal)value);
    }
}


public class ModifiedDateTimeTypeHandler : SqlMapper.TypeHandler<ModifiedDateTime>
{
    public override void SetValue(IDbDataParameter parameter, ModifiedDateTime value)
    {
        parameter.DbType = DbType.DateTime;
        parameter.Value = value.AsPrimitive();
    }

    public override ModifiedDateTime Parse(object value)
    {
        return new ModifiedDateTime((System.DateTime)value);
    }
}


public class RevisionNumberTypeHandler : SqlMapper.TypeHandler<RevisionNumber>
{
    public override void SetValue(IDbDataParameter parameter, RevisionNumber value)
    {
        parameter.DbType = DbType.Int16;
        parameter.Value = value.AsPrimitive();
    }

    public override RevisionNumber Parse(object value)
    {
        return new RevisionNumber((System.Int16)value);
    }
}


public class TaxRateTypeHandler : SqlMapper.TypeHandler<TaxRate>
{
    public override void SetValue(IDbDataParameter parameter, TaxRate value)
    {
        parameter.DbType = DbType.Decimal;
        parameter.Value = value.AsPrimitive();
    }

    public override TaxRate Parse(object value)
    {
        return new TaxRate((System.Decimal)value);
    }
}


public class QuantityTypeHandler : SqlMapper.TypeHandler<Quantity>
{
    public override void SetValue(IDbDataParameter parameter, Quantity value)
    {
        parameter.DbType = DbType.Int32;
        parameter.Value = value.AsPrimitive();
    }

    public override Quantity Parse(object value)
    {
        return new Quantity((System.Int32)value);
    }
}


public class FloatQuantityTypeHandler : SqlMapper.TypeHandler<FloatQuantity>
{
    public override void SetValue(IDbDataParameter parameter, FloatQuantity value)
    {
        parameter.DbType = DbType.Double;
        parameter.Value = value.AsPrimitive();
    }

    public override FloatQuantity Parse(object value)
    {
        return new FloatQuantity((System.Single)value);
    }
}


class TypeHandlerInitializer
{
    #pragma warning disable CA2255
    [ModuleInitializer]
    #pragma warning restore CA2255
    public static void Init()
    {
        SqlMapper.AddTypeHandler(new DateTypeHandler());
        SqlMapper.AddTypeHandler(new DaysTypeHandler());
        SqlMapper.AddTypeHandler(new DollarTypeHandler());
        SqlMapper.AddTypeHandler(new DollarPerGramTypeHandler());
        SqlMapper.AddTypeHandler(new EmployeeIdTypeHandler());
        SqlMapper.AddTypeHandler(new GramTypeHandler());
        SqlMapper.AddTypeHandler(new ModifiedDateTimeTypeHandler());
        SqlMapper.AddTypeHandler(new RevisionNumberTypeHandler());
        SqlMapper.AddTypeHandler(new TaxRateTypeHandler());
        SqlMapper.AddTypeHandler(new QuantityTypeHandler());
        SqlMapper.AddTypeHandler(new FloatQuantityTypeHandler());
    }
}
