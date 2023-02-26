using AdventureWorks.Purchasing.Production;

// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeTrailingCommaInMultilineLists
// ReSharper disable BuiltInTypeReferenceStyle
using MessagePack;
using MessagePack.Formatters;

namespace AdventureWorks.Purchasing.MagicOnion.Production;

public class ProductIdFormatter : IMessagePackFormatter<ProductId>
{
    public void Serialize(ref MessagePackWriter writer, ProductId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public ProductId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new ProductId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class ProductCategoryIdFormatter : IMessagePackFormatter<ProductCategoryId>
{
    public void Serialize(ref MessagePackWriter writer, ProductCategoryId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public ProductCategoryId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new ProductCategoryId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class ProductSubcategoryIdFormatter : IMessagePackFormatter<ProductSubcategoryId>
{
    public void Serialize(ref MessagePackWriter writer, ProductSubcategoryId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public ProductSubcategoryId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new ProductSubcategoryId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class UnitMeasureCodeFormatter : IMessagePackFormatter<UnitMeasureCode>
{
    public void Serialize(ref MessagePackWriter writer, UnitMeasureCode value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.String>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public UnitMeasureCode Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new UnitMeasureCode(options.Resolver.GetFormatterWithVerify<System.String>().Deserialize(ref reader, options));
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
        {typeof(ProductId), new ProductIdFormatter()},
        {typeof(ProductCategoryId), new ProductCategoryIdFormatter()},
        {typeof(ProductSubcategoryId), new ProductSubcategoryIdFormatter()},
        {typeof(UnitMeasureCode), new UnitMeasureCodeFormatter()},
    };

    internal static object GetFormatter(Type t)
    {
        return Formatters.TryGetValue(t, out var formatter) 
            ? formatter
            : null!;
    }
}
