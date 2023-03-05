using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Database;

public static class ConnectionStringProvider
{
    public static string Resolve(IConfiguration configuration)
    {
        const string connectionStringName = "ConnectionStrings:IdwDb";
        var connectionString = configuration[connectionStringName];
        if (connectionString is null)
        {
            throw new InvalidOperationException($"appsettings.jsonに{connectionStringName}が未設定です。");
        }

        return connectionString;
    }
}