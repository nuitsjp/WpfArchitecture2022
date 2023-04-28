using AdventureWorks.Hosting;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public static class ApplicationBuilderExtensions
{
    public static void UsePurchasingMagicOnion(this IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}