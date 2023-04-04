using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Database;

public static class ConnectionStringProvider
{
    public static string Resolve(IConfiguration configuration, string userId, string password)
    {
        const string dataSourceName = "Database:DataSource";
        var dataSource = configuration[dataSourceName];

        const string initialCatalogName = "Database:InitialCatalog";
        var initialCatalog = configuration[initialCatalogName];
        if (dataSource is null)
        {
            throw new InvalidOperationException($"appsettings.jsonに{dataSourceName}が未設定です。");
        }
        if (initialCatalog is null)
        {
            throw new InvalidOperationException($"appsettings.jsonに{initialCatalogName}が未設定です。");
        }

        return new SqlConnectionStringBuilder
        {
            DataSource = dataSource,
            InitialCatalog = initialCatalog,
            UserID = userId,
            Password = password,
            TrustServerCertificate = true
        }.ToString();
    }
}