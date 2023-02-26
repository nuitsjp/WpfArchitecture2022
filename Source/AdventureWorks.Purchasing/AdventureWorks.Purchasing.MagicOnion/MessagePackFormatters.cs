// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeTrailingCommaInMultilineLists
// ReSharper disable BuiltInTypeReferenceStyle
using MessagePack;
using MessagePack.Formatters;

namespace AdventureWorks.Purchasing.MagicOnion;

public class VendorIdFormatter : IMessagePackFormatter<VendorId>
{
    public void Serialize(ref MessagePackWriter writer, VendorId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public VendorId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new VendorId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class ShipMethodIdFormatter : IMessagePackFormatter<ShipMethodId>
{
    public void Serialize(ref MessagePackWriter writer, ShipMethodId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public ShipMethodId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new ShipMethodId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
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
        {typeof(VendorId), new VendorIdFormatter()},
        {typeof(ShipMethodId), new ShipMethodIdFormatter()},
    };

    internal static object GetFormatter(Type t)
    {
        return Formatters.TryGetValue(t, out var formatter) 
            ? formatter
            : null!;
    }
}
