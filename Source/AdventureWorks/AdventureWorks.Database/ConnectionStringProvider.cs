using AdventureWorks.Extensions;

namespace AdventureWorks.Database;

public static class ConnectionStringProvider
{
    public static string Resolve(IApplicationBuilder builder)
    {
        const string connectionStringName = "ConnectionStrings:IdwDb";
        var connectionString = builder.Configuration[connectionStringName];
        if (connectionString is null)
        {
            throw new InvalidOperationException($"appsettings.jsonに{connectionStringName}が未設定です。");
        }

        return connectionString;
    }
}