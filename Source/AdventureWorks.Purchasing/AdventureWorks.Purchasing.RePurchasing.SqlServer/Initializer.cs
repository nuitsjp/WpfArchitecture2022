using AdventureWorks.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Purchasing.RePurchasing.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        Database.Initializer.Initialize(builder);

        builder.Services.AddTransient(_ => new RePurchasingDatabase(builder.Configuration, "sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IRePurchasingQueryService, RePurchasingQueryService>();
    }
}

public class RePurchasingDatabase : Database.Database
{
    public RePurchasingDatabase(IConfiguration configuration, string userId, string password)
        : base(configuration, userId, password)
    {
    }
}