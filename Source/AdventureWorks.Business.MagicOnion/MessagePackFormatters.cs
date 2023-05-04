
// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeTrailingCommaInMultilineLists
// ReSharper disable BuiltInTypeReferenceStyle
using MessagePack;
using MessagePack.Formatters;

namespace AdventureWorks.Business.MagicOnion;

public class DateFormatter : IMessagePackFormatter<Date>
{
    public void Serialize(ref MessagePackWriter writer, Date value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.DateTime>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public Date Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new Date(options.Resolver.GetFormatterWithVerify<System.DateTime>().Deserialize(ref reader, options));
    }
}
public class DaysFormatter : IMessagePackFormatter<Days>
{
    public void Serialize(ref MessagePackWriter writer, Days value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public Days Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new Days(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class DollarFormatter : IMessagePackFormatter<Dollar>
{
    public void Serialize(ref MessagePackWriter writer, Dollar value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Decimal>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public Dollar Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new Dollar(options.Resolver.GetFormatterWithVerify<System.Decimal>().Deserialize(ref reader, options));
    }
}
public class DollarPerGramFormatter : IMessagePackFormatter<DollarPerGram>
{
    public void Serialize(ref MessagePackWriter writer, DollarPerGram value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Decimal>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public DollarPerGram Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new DollarPerGram(options.Resolver.GetFormatterWithVerify<System.Decimal>().Deserialize(ref reader, options));
    }
}
public class EmployeeIdFormatter : IMessagePackFormatter<EmployeeId>
{
    public void Serialize(ref MessagePackWriter writer, EmployeeId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public EmployeeId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new EmployeeId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class LoginIdFormatter : IMessagePackFormatter<LoginId>
{
    public void Serialize(ref MessagePackWriter writer, LoginId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.String>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public LoginId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new LoginId(options.Resolver.GetFormatterWithVerify<System.String>().Deserialize(ref reader, options));
    }
}
public class GramFormatter : IMessagePackFormatter<Gram>
{
    public void Serialize(ref MessagePackWriter writer, Gram value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Decimal>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public Gram Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new Gram(options.Resolver.GetFormatterWithVerify<System.Decimal>().Deserialize(ref reader, options));
    }
}
public class ModifiedDateTimeFormatter : IMessagePackFormatter<ModifiedDateTime>
{
    public void Serialize(ref MessagePackWriter writer, ModifiedDateTime value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.DateTime>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public ModifiedDateTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new ModifiedDateTime(options.Resolver.GetFormatterWithVerify<System.DateTime>().Deserialize(ref reader, options));
    }
}
public class RevisionNumberFormatter : IMessagePackFormatter<RevisionNumber>
{
    public void Serialize(ref MessagePackWriter writer, RevisionNumber value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int16>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public RevisionNumber Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new RevisionNumber(options.Resolver.GetFormatterWithVerify<System.Int16>().Deserialize(ref reader, options));
    }
}
public class TaxRateFormatter : IMessagePackFormatter<TaxRate>
{
    public void Serialize(ref MessagePackWriter writer, TaxRate value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Decimal>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public TaxRate Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new TaxRate(options.Resolver.GetFormatterWithVerify<System.Decimal>().Deserialize(ref reader, options));
    }
}
public class QuantityFormatter : IMessagePackFormatter<Quantity>
{
    public void Serialize(ref MessagePackWriter writer, Quantity value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public Quantity Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new Quantity(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class DoubleQuantityFormatter : IMessagePackFormatter<DoubleQuantity>
{
    public void Serialize(ref MessagePackWriter writer, DoubleQuantity value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Double>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public DoubleQuantity Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new DoubleQuantity(options.Resolver.GetFormatterWithVerify<System.Double>().Deserialize(ref reader, options));
    }
}
public class DecimalQuantityFormatter : IMessagePackFormatter<DecimalQuantity>
{
    public void Serialize(ref MessagePackWriter writer, DecimalQuantity value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Decimal>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public DecimalQuantity Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new DecimalQuantity(options.Resolver.GetFormatterWithVerify<System.Decimal>().Deserialize(ref reader, options));
    }
}

public class CustomResolver : IFormatterResolver
{
    // Resolver should be singleton.
    public static readonly IFormatterResolver Instance = new CustomResolver();

    private CustomResolver()
    {
    }

    // GetFormatter<T>'s get cost should be minimized so use type cache.
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return FormatterCache<T>.Formatter;
    }

    private static class FormatterCache<T>
    {
        public static readonly IMessagePackFormatter<T> Formatter;

        // generic's static constructor should be minimized for reduce type generation size!
        // use outer helper method.
        static FormatterCache()
        {
            Formatter = (IMessagePackFormatter<T>)CustomResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }
}

internal static class CustomResolverGetFormatterHelper
{
    static readonly Dictionary<Type, object> Formatters = new()
    {
        {typeof(Date), new DateFormatter()},
        {typeof(Days), new DaysFormatter()},
        {typeof(Dollar), new DollarFormatter()},
        {typeof(DollarPerGram), new DollarPerGramFormatter()},
        {typeof(EmployeeId), new EmployeeIdFormatter()},
        {typeof(LoginId), new LoginIdFormatter()},
        {typeof(Gram), new GramFormatter()},
        {typeof(ModifiedDateTime), new ModifiedDateTimeFormatter()},
        {typeof(RevisionNumber), new RevisionNumberFormatter()},
        {typeof(TaxRate), new TaxRateFormatter()},
        {typeof(Quantity), new QuantityFormatter()},
        {typeof(DoubleQuantity), new DoubleQuantityFormatter()},
        {typeof(DecimalQuantity), new DecimalQuantityFormatter()},
    };

    internal static object GetFormatter(Type t)
    {
        return Formatters.TryGetValue(t, out var formatter) 
            ? formatter
            : null!;
    }
}
