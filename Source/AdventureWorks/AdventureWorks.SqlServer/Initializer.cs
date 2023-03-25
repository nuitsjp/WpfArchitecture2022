using AdventureWorks.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.SqlServer;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient(_ => new AdventureWorksDatabase(builder.Configuration, "sa", "P@ssw0rd!"));
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
    }
}

public class AdventureWorksDatabase : Database.Database
{
    public AdventureWorksDatabase(IConfiguration configuration, string userId, string password) 
        : base(configuration, userId, password)
    {
    }
}