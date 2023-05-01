using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.SqlServer;

public static class ApplicationBuilderExtensions
{
    public static void UseBusinessSqlServer(this IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<AdventureWorksDatabase>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
    }
}