using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Database;

public static class ConnectionStringProvider
{
    public static string Resolve(string userId, string password)
    {
        return new SqlConnectionStringBuilder
        {
            DataSource = GetDataSource(),
            InitialCatalog = "AdventureWorks",
            UserID = userId,
            Password = password,
            TrustServerCertificate = true
        }.ToString();
    }

    static string GetDataSource()
    {
        const string name = "AdventureWorks.Database.DataSource";

        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process)
               ?? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User)
               ?? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine)
               // 開発環境などでローカルに共存している場合は環境変数を設定せずに利用できる。
               ?? "localhost";
    }
}