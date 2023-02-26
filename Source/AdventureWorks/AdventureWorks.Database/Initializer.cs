using AdventureWorks.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;

namespace AdventureWorks.Database;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        TypeHandlerInitializer.Initialize();

        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        const string connectionStringName = "ConnectionStrings:IdwDb";
        var connectionString = builder.Configuration[connectionStringName];
        if (connectionString is null)
        {
            throw new InvalidOperationException($"appsettings.jsonに{connectionStringName}が未設定です。");
        }
        builder.Services.AddTransient<IDatabase>(_ => new Database(connectionString));
    }
}