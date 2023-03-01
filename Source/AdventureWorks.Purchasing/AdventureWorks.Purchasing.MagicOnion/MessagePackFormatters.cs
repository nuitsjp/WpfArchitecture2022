// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeTrailingCommaInMultilineLists
// ReSharper disable BuiltInTypeReferenceStyle
using MessagePack;
using MessagePack.Formatters;

namespace AdventureWorks.Purchasing.MagicOnion;

public class AccountNumberFormatter : IMessagePackFormatter<AccountNumber>
{
    public void Serialize(ref MessagePackWriter writer, AccountNumber value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.String>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public AccountNumber Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new AccountNumber(options.Resolver.GetFormatterWithVerify<System.String>().Deserialize(ref reader, options));
    }
}
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
public class PurchaseOrderIdFormatter : IMessagePackFormatter<PurchaseOrderId>
{
    public void Serialize(ref MessagePackWriter writer, PurchaseOrderId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public PurchaseOrderId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new PurchaseOrderId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
    }
}
public class PurchaseOrderDetailIdFormatter : IMessagePackFormatter<PurchaseOrderDetailId>
{
    public void Serialize(ref MessagePackWriter writer, PurchaseOrderDetailId value, MessagePackSerializerOptions options)
    {
        options.Resolver.GetFormatterWithVerify<System.Int32>().Serialize(ref writer, value.AsPrimitive(), options);
    }

    public PurchaseOrderDetailId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        return new PurchaseOrderDetailId(options.Resolver.GetFormatterWithVerify<System.Int32>().Deserialize(ref reader, options));
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
        {typeof(AccountNumber), new AccountNumberFormatter()},
        {typeof(VendorId), new VendorIdFormatter()},
        {typeof(PurchaseOrderId), new PurchaseOrderIdFormatter()},
        {typeof(PurchaseOrderDetailId), new PurchaseOrderDetailIdFormatter()},
        {typeof(ShipMethodId), new ShipMethodIdFormatter()},
    };

    internal static object GetFormatter(Type t)
    {
        return Formatters.TryGetValue(t, out var formatter) 
            ? formatter
            : null!;
    }
}
