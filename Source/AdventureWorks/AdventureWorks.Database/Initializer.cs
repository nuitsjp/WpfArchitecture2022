using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Database;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDatabase>(_ => new Database(ConnectionStringProvider.Resolve(builder.Configuration)));
    }
}