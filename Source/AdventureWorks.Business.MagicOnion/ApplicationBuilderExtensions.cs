using AdventureWorks.Hosting;

namespace AdventureWorks.Business.MagicOnion;

public static class ApplicationBuilderExtensions
{
    public static void UseBusinessMagicOnion(this IApplicationBuilder builder)
    {
        builder.AddFormatterResolver(CustomResolver.Instance);
    }
}
