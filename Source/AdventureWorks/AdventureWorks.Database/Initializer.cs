using AdventureWorks.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Database;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddTransient<IDatabase>(_ => new Database(ConnectionStringProvider.Resolve(builder)));
    }
}