using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

public static class ApplicationBuilderExtensions
{
    public static void UseRePurchasingSqlServer(this IApplicationBuilder builder)
    {
        builder.Services.AddTransient(_ => new RePurchasingDatabase("sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IRequiringPurchaseProductQuery, RequiringPurchaseProductQuery>();
    }
}