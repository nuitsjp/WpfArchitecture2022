using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Business.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient(_ => new AdventureWorksDatabase(builder.Configuration, "sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
    }
}